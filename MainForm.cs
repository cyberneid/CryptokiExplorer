using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.Threading;
using System.Security.Cryptography.X509Certificates;
using System.Runtime.InteropServices;
using System.IO;
using System.Security.Cryptography;
using Cyberneid.NCryptoki;

namespace CryptokiExplorer
{
    public partial class MainForm : Form
    {
        enum ViewState
        {
            None = 0,
            Info,
            Keys,
            Certs,
            Datas,
            Mechs
        };

        // public static NProperties props;

        Cryptoki _cryptoki;
        SlotList _slotList;
        List<Session> _sessionList;
        ViewState _currentView;

        bool _bMonitorEnable;
        Thread _thread;

        public MainForm()
        {
            InitializeComponent();
        }

        private void MainForm_Load(object sender, EventArgs e)
        {            
            try
            {
                string path = Properties.Settings.Default.CryptokiPath; //  "C:\\Windows\\System32\\CIEPKI.dll"; //props.GetProperty("cryptoki", "");
                path = path.Replace("\\", "/");

                _cryptoki = new Cryptoki(path);

                Init();
                btnInfo.Enabled = true;

                _currentView = ViewState.None;

                //toolStripStatusLabel.Text = "Success";
                //toolStripStatusLabel.Image = Properties.Resources.accept;
            }
            catch (CryptokiException ex)
            {
                toolStripStatusLabel.Text = ex.ErrorString;
                toolStripStatusLabel.Image = Properties.Resources.delete;
                showError(ex);
            }
            catch (Exception ex)
            {
                toolStripStatusLabel.Text = ex.Message;
                toolStripStatusLabel.Image = Properties.Resources.delete;
                showError(ex);
            }
        }

        private int GetSelectedReaderIndex()
        {
            return comboBoxReader.SelectedIndex;
        }

        private void SetSelectedReader(string reader)
        {
            comboBoxReader.SelectedText = reader;
        }

        private void OnReader(bool inserted)
        {
            btnMechanism.Enabled = inserted;
            btnCertificate.Enabled = inserted;
            btnKey.Enabled = inserted;
            btnData.Enabled = inserted;
            btnImport.Enabled = false;
            btnExport.Enabled = false;
            btnDelete.Enabled = false;
            btnInfo.Enabled = true;

            btnVerifyPIN.Text = "Login";
            btnVerifyPIN.Enabled = inserted;
            textBoxPIN.Text = "";
            textBoxPIN.Enabled = inserted;

            if (inserted)
            {                
                switch(_currentView)
                {
                    case ViewState.Certs:
                        btnCertificate_Click(null, null);
                        break;
                    case ViewState.Keys:
                        btnKey_Click(null, null);
                        break;
                    case ViewState.Datas:
                        btnData_Click(null, null);
                        break;

                    case ViewState.Info:
                        btnInfo_Click(null, null);
                        break;

                    case ViewState.Mechs:
                        btnMechanism_Click(null,  null);
                        break;
                }

                toolStripStatusLabel.Text = "Token inserted";
                toolStripStatusLabel.Image = Properties.Resources.accept;
                pictureBoxReader.Image = Properties.Resources.accept;
            }
            else
            {
                toolStripStatusLabel.Text = "Token not inserted";
                toolStripStatusLabel.Image = Properties.Resources.error;
                pictureBoxReader.Image = Properties.Resources.error;
                pictureBoxPIN.Image = Properties.Resources.error;
                listViewContent.Items.Clear();
            }          
        }
           

        private void ExitApp()
        {
            _bMonitorEnable = false;
            if (_cryptoki != null)
            {
                try
                {
                    _cryptoki.Finalize(IntPtr.Zero);
                }
                catch (Exception)
                {

                }
                
            }

            Thread.Sleep(2000);

            _thread?.Abort();

            Dispose();
            Application.Exit();
        }

        delegate int DGetSelectedReaderIndex();
        delegate void DSetSelectedReader(string reader);
        delegate void OnReaderDelegate(bool enable);
        delegate void SetCursorDelegate(Cursor cursor);
        delegate Exception MonitorDelegate();
        delegate void InitDelegate();

        private void setCursor(Cursor cursor)
        {
            this.Cursor = cursor;
            this.Enabled = cursor.Equals(Cursors.Default);            
        }

        private void Init()
        {            
            if (_cryptoki == null)
            {
                return;
            }

            int nRet = _cryptoki.Initialize();
            if (nRet != 0)
            {
                toolStripStatusLabel.Text = CryptokiException.GetErrorString(nRet);
                toolStripStatusLabel.Image = Properties.Resources.delete;
                //showError(nRet);
                return;// new Exception(CryptokiException.GetErrorString(nRet));
            }

            comboBoxReader.Items.Clear();
            comboBoxReader.Text = "";

            this.Enabled = true;

            _slotList = _cryptoki.Slots;

            if (_slotList.Count > 0)
            {                              
                _sessionList = new List<Session>();
                foreach (Slot slot in _slotList)
                {
                    comboBoxReader.Items.Add(slot.Info.Description);
                    _sessionList.Add(null);
                }
            
                comboBoxReader.SelectedIndex = 0;
                OnReader(_slotList[0].IsTokenPresent);

                btnRefresh.Enabled = true;

                _thread = new Thread(new ThreadStart(this.readerMonitor));
                _thread.Name = "Monitoring";
                _thread.Start();
            }
            else
            {
                OnReader(false);                
                //btnRefresh.Enabled = false;
                toolStripStatusLabel.Text = "No reader detected";
                toolStripStatusLabel.Image = Properties.Resources.delete;
                pictureBoxReader.Image = Properties.Resources.delete;
                comboBoxReader.Items.Clear();
                comboBoxReader.SelectedIndex = -1;
                comboBoxReader.Text = "";
                _cryptoki.Finalize(IntPtr.Zero);
                return;// new Exception("No reader detected");
            }

            return;
        }

        private void readerMonitor()
        {
            SetCursorDelegate setCursorDelegate = new SetCursorDelegate(setCursor);
            int n;
            _bMonitorEnable = true;
            try
            {
                while (_bMonitorEnable)
                {                
                    Slot slot = _cryptoki.WaitForSlotEvent(0);
                    n = (int)Invoke(new DGetSelectedReaderIndex(GetSelectedReaderIndex));
                    if (slot.SlotID == _slotList[n].SlotID)
                    {
                        _currentView = ViewState.None;

                        if (slot.IsTokenPresent)
                        {
                            Invoke(new OnReaderDelegate(OnReader), true);
                        }
                        else // token removed
                        {
                            Invoke(new OnReaderDelegate(OnReader), false);
                            Session session = _sessionList[n];
                            if (session != null)
                                session.Close();

                            _sessionList[n] = null;

                            _cryptoki.Finalize(IntPtr.Zero);
                            Invoke(new InitDelegate(Init));
                            return;
                        }                        
                    }                                        
                }
            }
            catch (CryptokiException)
            {
                
                try
                {
                    if (_bMonitorEnable)
                    {
                        Invoke(new OnReaderDelegate(OnReader), false);
                        _cryptoki.Finalize(IntPtr.Zero);
                        //showError("exit");
                        Invoke(new InitDelegate(Init));
                    }
                }
                catch (Exception)
                {
                }

                _bMonitorEnable = false;
                //Thread.Sleep(2000);

                //showError(ex);
                //toolStripStatusLabel.Text = ex.ErrorString;
                //toolStripStatusLabel.Image = Properties.Resources.delete;
                
                //_cryptoki.Finalize(IntPtr.Zero);

                //Invoke(new InitDelegate(Init));                                                                  
            }
            catch (Exception)
            {
                try
                {
                    if (_bMonitorEnable)
                    {
                        Invoke(new OnReaderDelegate(OnReader), false);
                        _cryptoki.Finalize(IntPtr.Zero);
                        Invoke(new InitDelegate(Init));    
                    }
                }
                catch (Exception)
                {
                }

                _bMonitorEnable = false;

                //Thread.Sleep(2000);
                //showError(ex);
                //toolStripStatusLabel.Text = ex.Message;
                //toolStripStatusLabel.Image = Properties.Resources.delete;
                //_cryptoki.Finalize(IntPtr.Zero);
                //Invoke(new InitDelegate(Init));                                      
                
            }
        
        }       

        private void showError(int nErr)
        {
            toolStripStatusLabel.Text = CryptokiException.GetErrorString(nErr);
            toolStripStatusLabel.Image = Properties.Resources.delete;
            //MessageBox.Show(this, CryptokiException.GetErrorString(nErr), "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
        }

        private void showError(CryptokiException ex)
        {
            toolStripStatusLabel.Text = ex.ErrorString;
            toolStripStatusLabel.Image = Properties.Resources.delete;
            //MessageBox.Show(this, ex.ErrorString, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
        }

        private void showError(Exception ex)
        {
            toolStripStatusLabel.Text = ex.Message;
            toolStripStatusLabel.Image = Properties.Resources.delete;
            //MessageBox.Show(this, ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
        }

        private void showError(string msg)
        {
            MessageBox.Show(this, msg, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
        }

        private void showMessage(string msg)
        {
            MessageBox.Show(this, msg, "Message", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        private void btnInfo_Click(object sender, EventArgs e)
        {
            _currentView = ViewState.Info;
            listViewContent.Items.Clear();
            listViewContent.Columns.Clear();
            listViewContent.Columns.Add("Attribute");
            listViewContent.Columns.Add("Value");
            listViewContent.View = View.Details;

            CryptokiInfo info;
            ListViewItem lvi;

            try
            {
                info = _cryptoki.Info;
            }
            catch (CryptokiException)
            {
                _cryptoki.Initialize();
                info = _cryptoki.Info;
            }
            
            try
            {
                listViewContent.Items.Add(new ListViewItem(new string[] { "Library File", Properties.Settings.Default.CryptokiPath}, 1));
                listViewContent.Items.Add(new ListViewItem(new string[] { "Library Name", info.LibDescription }, 1));
                listViewContent.Items.Add(new ListViewItem(new string[] { "Library Version", info.LibVersion }, 1));
                listViewContent.Items.Add(new ListViewItem(new string[] { "Library Manufacturer", info.ManufacturerID }, 1));
                listViewContent.Items.Add(new ListViewItem(new string[] { "PKCS#11 Version", info.Version }, 1));

                if (_slotList != null && _slotList.Count > 0)
                {
                    Slot slot = _slotList[comboBoxReader.SelectedIndex];

                    lvi = listViewContent.Items.Add(new ListViewItem(new string[] { "Reader", slot.Info.Description }, 0));
                    lvi.Tag = slot;

                    if (slot.IsTokenPresent)
                    {
                        Token token = slot.Token;
                        listViewContent.Items.Add(new ListViewItem(new string[] { "Token Label", token.Info.Label }, 6));
                        listViewContent.Items.Add(new ListViewItem(new string[] { "Token Model", token.Info.Model }, 6));
                        listViewContent.Items.Add(new ListViewItem(new string[] { "Token Manufacturer", token.Info.ManufacturerID }, 6));
                    }
                }

                toolStripStatusLabel.Text = "Success";
                toolStripStatusLabel.Image = Properties.Resources.accept;
            }
            catch (CryptokiException ex)
            {
                toolStripStatusLabel.Text = ex.ErrorString;
                toolStripStatusLabel.Image = Properties.Resources.delete;
                //showError(ex);
            }
            catch (Exception ex)
            {
                toolStripStatusLabel.Text = ex.Message;
                toolStripStatusLabel.Image = Properties.Resources.delete;
                //showError(ex);
            }

            listViewContent.AutoResizeColumns(ColumnHeaderAutoResizeStyle.ColumnContent);

            btnExport.Enabled = false;
            btnImport.Enabled = false;
            btnDelete.Enabled = false;


        }

        private void btnMechanism_Click(object sender, EventArgs e)
        {
            _currentView = ViewState.Mechs;
            listViewContent.Items.Clear();
            listViewContent.Columns.Clear();
            listViewContent.Columns.Add("Mechanism");
            
            listViewContent.View = View.Details;

            try
            {
                if (_slotList.Count > 0)
                {
                    Slot slot = _slotList[comboBoxReader.SelectedIndex];

                    if (slot.IsTokenPresent)
                    {
                        Token token = slot.Token;
                        MechanismList mechList = token.MechanismList;

                        foreach (MechanismInfo m in mechList)
                        {
                            listViewContent.Items.Add(new ListViewItem(new string[] { "" + m.MechanismName }, 5));
                        }
                    }
                }

                toolStripStatusLabel.Text = "Success";
                toolStripStatusLabel.Image = Properties.Resources.accept;

            }
            catch (CryptokiException ex)
            {
                toolStripStatusLabel.Text = ex.ErrorString;
                toolStripStatusLabel.Image = Properties.Resources.delete;
                //showError(ex);
            }
            catch (Exception ex)
            {
                toolStripStatusLabel.Text = ex.Message;
                toolStripStatusLabel.Image = Properties.Resources.delete;
                //showError(ex);
            }

            listViewContent.AutoResizeColumns(ColumnHeaderAutoResizeStyle.ColumnContent);

            btnExport.Enabled = false;
            btnImport.Enabled = false;
            btnDelete.Enabled = false;
        }
        
        private void btnKey_Click(object sender, EventArgs e)
        {
            _currentView = ViewState.Keys;
            listViewContent.Items.Clear();
            listViewContent.Columns.Clear();
            
            listViewContent.Columns.Add("Label");
            listViewContent.Columns.Add("Class");
            listViewContent.Columns.Add("Type");

            listViewContent.View = View.Details;

            ListViewItem lvi;
            try
            {
                Session session = CurrentSession;
                if (session == null)
                {
                    toolStripStatusLabel.Text = "Device removed";
                    toolStripStatusLabel.Image = Properties.Resources.delete;
                    //showError(CryptokiException.CKR_DEVICE_REMOVED);
                    return;
                }

                CryptokiCollection templatePubKey = new CryptokiCollection();
                templatePubKey.Add(new ObjectAttribute(ObjectAttribute.CKA_CLASS, CryptokiObject.CKO_PUBLIC_KEY));

                Key key;
                CryptokiCollection objs = session.Objects.Find(templatePubKey, 100);

                foreach (CryptokiObject obj in objs)
                {
                    key = (Key)obj;
                    lvi = listViewContent.Items.Add(new ListViewItem(new string[] { key.Label, key.ClassName, key.KeyTypeName }, 3));
                    lvi.Tag = key;
                }

                CryptokiCollection templatePriKey = new CryptokiCollection();
                templatePriKey.Add(new ObjectAttribute(ObjectAttribute.CKA_CLASS, CryptokiObject.CKO_PRIVATE_KEY));

                objs = session.Objects.Find(templatePriKey, 100);

                foreach (CryptokiObject obj in objs)
                {
                    key = (Key)obj;
                    lvi = listViewContent.Items.Add(new ListViewItem(new string[] { key.Label, key.ClassName, key.KeyTypeName }, 7));
                    lvi.Tag = key;
                }

                CryptokiCollection templateKey = new CryptokiCollection();
                templateKey.Add(new ObjectAttribute(ObjectAttribute.CKA_CLASS, CryptokiObject.CKO_SECRET_KEY));

                objs = session.Objects.Find(templateKey, 100);

                foreach (CryptokiObject obj in objs)
                {
                    key = (Key)obj;
                    lvi = listViewContent.Items.Add(new ListViewItem(new string[] { key.Label, key.ClassName, key.KeyTypeName }, 3));
                    lvi.Tag = key;

                }

                toolStripStatusLabel.Text = "Success";
                toolStripStatusLabel.Image = Properties.Resources.accept;

            }
            catch (CryptokiException ex)
            {
                toolStripStatusLabel.Text = ex.ErrorString;
                toolStripStatusLabel.Image = Properties.Resources.delete;
                //showError(ex);
            }
            catch (Exception ex)
            {
                toolStripStatusLabel.Text = ex.Message;
                toolStripStatusLabel.Image = Properties.Resources.delete;
                //showError(ex);
            }

            listViewContent.AutoResizeColumns(ColumnHeaderAutoResizeStyle.ColumnContent);

            btnExport.Enabled = false;
            btnImport.Enabled = true;// CurrentSession.IsLoggedIn;
            btnDelete.Enabled = listViewContent.Items.Count > 0;// CurrentSession.IsLoggedIn;
        }

        private void btnVerifyPIN_Click(object sender, EventArgs e)
        {
            try
            {
                
                Slot slot = _slotList[comboBoxReader.SelectedIndex];

                Session session = CurrentSession;
                if(session == null)
                {
                    toolStripStatusLabel.Text = "Device removed";
                    toolStripStatusLabel.Image = Properties.Resources.delete;
                    //showError(CryptokiException.CKR_DEVICE_REMOVED);
                    return;
                }

                int nRet;

                if (session.IsLoggedIn)
                {
                    nRet = session.Logout();
                    if (nRet != 0)
                    {
                        showError(nRet);
                        //menuStrip1.Items[1].Enabled = false;
                        return;
                    }

                    btnVerifyPIN.Text = "Login";
                    textBoxPIN.Enabled = true;

                    pictureBoxPIN.Image = Properties.Resources.delete;


                }
                else
                {
                    if (textBoxPIN.Text.Length > 0)
                    {
                        nRet = session.Login(Session.CKU_USER, textBoxPIN.Text);
                        if (nRet != 0)
                        {
                            showError(nRet);
                            showError(CryptokiException.GetErrorString(nRet));                            
                            return;
                        }

                        btnVerifyPIN.Text = "Logout";
                        textBoxPIN.Enabled = false;
                        pictureBoxPIN.Image = Properties.Resources.accept;                                               
                    }
                }

                switch (_currentView)
                {
                    case ViewState.Certs:
                        btnCertificate_Click(null, null);
                        break;

                    case ViewState.Keys:
                        btnKey_Click(null, null);
                        break;

                    case ViewState.Datas:
                        btnData_Click(null, null);
                        break;

                    case ViewState.Info:
                        btnInfo_Click(null, null);
                        break;

                    case ViewState.Mechs:
                        btnMechanism_Click(null, null);
                        break;
                }

                toolStripStatusLabel.Text = "Success";
                toolStripStatusLabel.Image = Properties.Resources.accept;

            }
            catch (CryptokiException ex)
            {
                toolStripStatusLabel.Text = ex.ErrorString;
                toolStripStatusLabel.Image = Properties.Resources.delete;
                //showError(ex);
            }
            catch (Exception ex)
            {
                toolStripStatusLabel.Text = ex.Message;
                toolStripStatusLabel.Image = Properties.Resources.delete;
                //showError(ex);
            }
        }

        public Session CurrentSession
        {
            get
            {
                if (_slotList == null)
                    return null;

                if (_slotList.Count > 0)
                {
                    Slot slot = _slotList[comboBoxReader.SelectedIndex];
                    Session session = _sessionList[comboBoxReader.SelectedIndex];

                    if (!slot.IsTokenPresent)
                    {
                        if (session != null)
                        {
                            session.Close();
                            _sessionList[comboBoxReader.SelectedIndex] = null;
                        }

                        return null;
                    }

                    if (session == null)
                    {
                        session = slot.Token.OpenSession(Session.CKF_SERIAL_SESSION | Session.CKF_RW_SESSION);
                        _sessionList[comboBoxReader.SelectedIndex] = session;
                    }

                    return session;
                }
                else
                {
                    return null;
                }
            }
        }

        private void btnCertificate_Click(object sender, EventArgs e)
        {
            _currentView = ViewState.Certs;
            listViewContent.Items.Clear();
            listViewContent.Columns.Clear();
            
            listViewContent.Columns.Add("Label");
            listViewContent.Columns.Add("Class");
            listViewContent.Columns.Add("Type");

            listViewContent.View = View.Details;

            try
            {
                Session session = CurrentSession;
                if (session == null)
                {
                    toolStripStatusLabel.Text = "Device removed";
                    toolStripStatusLabel.Image = Properties.Resources.delete;
                    //showError(CryptokiException.CKR_DEVICE_REMOVED);
                    return;
                }

                CryptokiCollection template = new CryptokiCollection();
                template.Add(new ObjectAttribute(ObjectAttribute.CKA_CLASS, CryptokiObject.CKO_CERTIFICATE));

                Certificate cert;
                CryptokiCollection objs = session.Objects.Find(template, 100);

                foreach (CryptokiObject obj in objs)
                {
                    cert = (Certificate)obj;
                    ListViewItem l = listViewContent.Items.Add(new ListViewItem(new string[] { cert.Label, cert.ClassName, cert.CertificateTypeName, }, 2));
                    l.Tag = cert;
                }

                toolStripStatusLabel.Text = "Success";
                toolStripStatusLabel.Image = Properties.Resources.accept;

            }
            catch (CryptokiException ex)
            {
                toolStripStatusLabel.Text = ex.ErrorString;
                toolStripStatusLabel.Image = Properties.Resources.delete;
                //showError(ex);
            }
            catch (Exception ex)
            {
                toolStripStatusLabel.Text = ex.Message;
                toolStripStatusLabel.Image = Properties.Resources.delete;
                //showError(ex);
            }

            listViewContent.AutoResizeColumns(ColumnHeaderAutoResizeStyle.ColumnContent);

            btnExport.Enabled = listViewContent.Items.Count > 0;
            btnImport.Enabled = true;// CurrentSession.IsLoggedIn;
            btnDelete.Enabled = listViewContent.Items.Count > 0;
        }

        private void btnData_Click(object sender, EventArgs e)
        {
            _currentView = ViewState.Datas;
            listViewContent.Items.Clear();
            listViewContent.Columns.Clear();
            listViewContent.Columns.Add("Label");
            listViewContent.Columns.Add("Application");
            
            listViewContent.View = View.Details;

            try
            {
                Session session = CurrentSession;
                if (session == null)
                {
                    toolStripStatusLabel.Text = "Device removed";
                    toolStripStatusLabel.Image = Properties.Resources.delete;
                    //showError(CryptokiException.CKR_DEVICE_REMOVED);
                    return;
                }

                CryptokiCollection template = new CryptokiCollection();
                template.Add(new ObjectAttribute(ObjectAttribute.CKA_CLASS, CryptokiObject.CKO_DATA));

                Data data;
                CryptokiCollection objs = session.Objects.Find(template, 100);

                foreach (CryptokiObject obj in objs)
                {
                    data = (Data)obj;
                    ListViewItem lvi = listViewContent.Items.Add(new ListViewItem(new string[] { data.Label, data.Application }, 4));
                    lvi.Tag = data;
                }

                toolStripStatusLabel.Text = "Success";
                toolStripStatusLabel.Image = Properties.Resources.accept;

            }
            catch (CryptokiException ex)
            {
                toolStripStatusLabel.Text = ex.ErrorString;
                toolStripStatusLabel.Image = Properties.Resources.delete;
                //showError(ex);
            }
            catch (Exception ex)
            {
                toolStripStatusLabel.Text = ex.Message;
                toolStripStatusLabel.Image = Properties.Resources.delete;
                //showError(ex);
            }

            listViewContent.AutoResizeColumns(ColumnHeaderAutoResizeStyle.ColumnContent);

            btnExport.Enabled = listViewContent.Items.Count > 0;
            btnImport.Enabled = true;// CurrentSession.IsLoggedIn;
            btnDelete.Enabled = listViewContent.Items.Count > 0;
        }

        private void btnExport_Click(object sender, EventArgs e)
        {
            if (listViewContent.SelectedItems.Count == 0)
            {
                showError("Select an object to export!");
                return;
            }

            try
            {
                CryptokiObject obj = (CryptokiObject)listViewContent.SelectedItems[0].Tag;
                if (obj is Data)
                {
                    SaveFileDialog dlgSave = new SaveFileDialog();
                    if (dlgSave.ShowDialog(this) == DialogResult.OK)
                    {
                        byte[] data = ((Data)obj).DataValue;
                        Stream stream = dlgSave.OpenFile();
                        stream.Write(data, 0, data.Length);
                        stream.Close();
                    }

                    dlgSave.Dispose();                    
                    showMessage("Data exported succesfully!");
                }
                else if (obj is Certificate)
                {
                    SaveFileDialog dlgSave = new SaveFileDialog();
                    dlgSave.AddExtension = true;
                    dlgSave.DefaultExt = "cer";
                    dlgSave.Filter = "Certificate File (*.cer)|*.cer";
                    dlgSave.Title = "Save Certificate";

                    if (dlgSave.ShowDialog(this) == DialogResult.OK)
                    {
                        byte[] data = ((Certificate)obj).CertificateValue;
                        Stream stream = dlgSave.OpenFile();
                        stream.Write(data, 0, data.Length);
                        stream.Close();
                    }

                    dlgSave.Dispose();
                    showMessage("Certificate exported succesfully!");

                }

                toolStripStatusLabel.Text = "Success";
                toolStripStatusLabel.Image = Properties.Resources.accept;

            }
            catch (CryptokiException ex)
            {
                toolStripStatusLabel.Text = ex.ErrorString;
                toolStripStatusLabel.Image = Properties.Resources.delete;
                //showError(ex);
            }
            catch (Exception ex)
            {
                toolStripStatusLabel.Text = ex.Message;
                toolStripStatusLabel.Image = Properties.Resources.delete;
                //showError(ex);
            }
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            if (listViewContent.SelectedItems.Count == 0)
            {
                showError("Select an object to delete!");
                return;
            }

            if(MessageBox.Show("Do you want to delete the object?", "Delete", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            {
                try
                {
                    CryptokiObject obj = (CryptokiObject)listViewContent.SelectedItems[0].Tag;
                    Session session = CurrentSession;
                    if (session == null)
                    {
                        toolStripStatusLabel.Text = "Device removed";
                        toolStripStatusLabel.Image = Properties.Resources.delete;
                        //showError(CryptokiException.CKR_DEVICE_REMOVED);
                        return;
                    }

                    int nRet = session.Objects.Destroy(obj);
                    if (nRet != 0)
                        showError(nRet);

                    if (obj is Certificate)
                        btnCertificate_Click(null, null);
                    else if (obj is Data)
                        btnData_Click(null, null);
                    else
                        btnKey_Click(null, null);

                    showMessage("Object deleted succesfully!");

                    toolStripStatusLabel.Text = "Object deleted succesfully";
                    toolStripStatusLabel.Image = Properties.Resources.accept;
                }
                catch (CryptokiException ex)
                {
                    toolStripStatusLabel.Text = ex.ErrorString;
                    toolStripStatusLabel.Image = Properties.Resources.delete;
                    //showError(ex);
                }
                catch (Exception ex)
                {
                    toolStripStatusLabel.Text = ex.Message;
                    toolStripStatusLabel.Image = Properties.Resources.delete;
                    //showError(ex);
                }

            }
        }

        private void listViewContent_DoubleClick(object sender, EventArgs e)
        {
            Explore();
        }

        private void Explore()
        {
            try
            {
                switch (_currentView)
                {
                    case ViewState.Certs:
                    case ViewState.Datas:
                    case ViewState.Keys:
                        ObjectBrowser objb = new ObjectBrowser(listViewContent.SelectedItems[0].Tag);
                        objb.ShowDialog();
                        objb.Dispose();
                        break;

                    case ViewState.Info:
                        if (listViewContent.SelectedItems[0].Tag is Slot)
                        {
                            objb = new ObjectBrowser(listViewContent.SelectedItems[0].Tag);
                            objb.ShowDialog();
                            objb.Dispose();
                        }
                        break;
                }

                toolStripStatusLabel.Text = "Success";
                toolStripStatusLabel.Image = Properties.Resources.accept;
            }
            catch (CryptokiException ex)
            {
                toolStripStatusLabel.Text = ex.ErrorString;
                toolStripStatusLabel.Image = Properties.Resources.delete;
                showError(ex);
            }
            catch (Exception ex)
            {
                toolStripStatusLabel.Text = ex.Message;
                toolStripStatusLabel.Image = Properties.Resources.delete;
                showError(ex);
            }
        }

        public const int CRYPTUI_DISABLE_ADDTOSTORE = 0x00000010;

        [DllImport("CryptUI.dll", CharSet = CharSet.Auto, SetLastError = true)]
        private static extern Boolean CryptUIDlgViewCertificate(
            ref CRYPTUI_VIEWCERTIFICATE_STRUCT pCertViewInfo,
            ref bool pfPropertiesChanged
        );

        private struct CRYPTUI_VIEWCERTIFICATE_STRUCT
        {
            public int dwSize;
            public IntPtr hwndParent;
            public int dwFlags;
            [MarshalAs(UnmanagedType.LPWStr)]
            public String szTitle;
            public IntPtr pCertContext;
            public IntPtr rgszPurposes;
            public int cPurposes;
            public IntPtr pCryptProviderData; // or hWVTStateData
            public Boolean fpCryptProviderDataTrustedUsage;
            public int idxSigner;
            public int idxCert;
            public Boolean fCounterSigner;
            public int idxCounterSigner;
            public int cStores;
            public IntPtr rghStores;
            public int cPropSheetPages;
            public IntPtr rgPropSheetPages;
            public int nStartPage;
        }

        private void ShowCertificate(byte[] certRaw)
        {
            // Get the cert
            X509Certificate2 cert = new X509Certificate2(certRaw);

            // Show the cert
            CRYPTUI_VIEWCERTIFICATE_STRUCT certViewInfo = new CRYPTUI_VIEWCERTIFICATE_STRUCT();
            certViewInfo.dwSize = Marshal.SizeOf(certViewInfo);
            certViewInfo.pCertContext = cert.Handle;
            certViewInfo.szTitle = "Certificate Info";
            certViewInfo.dwFlags = CRYPTUI_DISABLE_ADDTOSTORE;
            certViewInfo.nStartPage = 0;
            certViewInfo.hwndParent = this.Handle;
            bool fPropertiesChanged = false;
            CryptUIDlgViewCertificate(ref certViewInfo, ref fPropertiesChanged);
            //if (!CryptUIDlgViewCertificate(ref certViewInfo, ref fPropertiesChanged))
            //{                
            //    MessageBox.Show(new Win32Exception().Message);
            //}
        }

        private void aboutToolStripMenuItem_Click(object sender, EventArgs e)
        {
            AboutForm f = new AboutForm();
            f.ShowDialog(this);
            f.Dispose();
        }

        private void changePINToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ChangePINForm dlg = new ChangePINForm();

            try
            {
                if (dlg.ShowDialog(this) == DialogResult.OK)
                {
                    Session session = CurrentSession;
                    if (session == null)
                    {
                        toolStripStatusLabel.Text = "Device removed";
                        toolStripStatusLabel.Image = Properties.Resources.delete;
                        showError(CryptokiException.CKR_DEVICE_REMOVED);
                        dlg.Dispose();
                        return;
                    }

                    int nRet = session.SetPIN(dlg.textBoxOldPIN.Text, dlg.textBoxNewPIN1.Text);
                    if (nRet != 0)
                        showError(CryptokiException.GetErrorString(nRet));
                    else
                        showMessage("PIN changed succesfully!");

                }

                dlg.Dispose();

                toolStripStatusLabel.Text = "Success";
                toolStripStatusLabel.Image = Properties.Resources.accept;
            }
            catch (CryptokiException ex)
            {
                dlg.Dispose();
                toolStripStatusLabel.Text = ex.ErrorString;
                toolStripStatusLabel.Image = Properties.Resources.delete;
                showError(ex);
            }
            catch (Exception ex)
            {
                dlg.Dispose();
                toolStripStatusLabel.Text = ex.Message;
                toolStripStatusLabel.Image = Properties.Resources.delete;
                showError(ex);
            }
        }
        

        private void comboBoxReader_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                if (_slotList.Count > 0)
                {
                    Slot slot = _slotList[comboBoxReader.SelectedIndex];
                    if (slot.IsTokenPresent)
                    {
                        pictureBoxReader.Image = Properties.Resources.accept;
                        toolStripStatusLabel.Text = "Reader connected and Token inserted";
                        toolStripStatusLabel.Image = Properties.Resources.accept;
                        Session session = _sessionList[comboBoxReader.SelectedIndex];
                        if (session != null)
                        {
                            btnVerifyPIN.Enabled = !session.IsLoggedIn;
                            textBoxPIN.Enabled = !session.IsLoggedIn;
                        }
                        else
                        {
                            btnVerifyPIN.Enabled = true;
                            textBoxPIN.Enabled = true;
                        }

                        OnReader(true);
                    }
                    else
                    {
                        pictureBoxReader.Image = Properties.Resources.error;
                        toolStripStatusLabel.Text = "Reader connected and Token not inserted";
                        toolStripStatusLabel.Image = Properties.Resources.error;
                        btnVerifyPIN.Enabled = false;
                        textBoxPIN.Enabled = false;
                    }
                }
                else
                {
                    toolStripStatusLabel.Text = "No Slot available";
                    toolStripStatusLabel.Image = Properties.Resources.accept;
                }
            }
            catch (CryptokiException ex)
            {
                toolStripStatusLabel.Text = ex.ErrorString;
                toolStripStatusLabel.Image = Properties.Resources.delete;
                showError(ex);
            }
            catch (Exception ex)
            {
                toolStripStatusLabel.Text = ex.Message;
                toolStripStatusLabel.Image = Properties.Resources.delete;
                showError(ex);
            }
        }

        private void btnImport_Click(object sender, EventArgs e)
        {
            Session session = CurrentSession;
            if (session == null)
            {
                toolStripStatusLabel.Text = "Device removed";
                toolStripStatusLabel.Image = Properties.Resources.delete;
                //showError(CryptokiException.CKR_DEVICE_REMOVED);
                return;
            }

            OpenFileDialog dlgOpen = new OpenFileDialog();

            try
            {
                dlgOpen.AddExtension = true;

                if (_currentView == ViewState.Certs)
                {
                    dlgOpen.DefaultExt = "cer";
                    dlgOpen.Filter = "Certificate File (*.cer)|*.cer|Certificate File (*.crt)|*.crt|Personal Information Exchange File (*.pfx)|*.pfx|PKCS#12 File (*.p12)|*.p12|All Files (*.*)|*.*";
                    if (dlgOpen.ShowDialog(this) == DialogResult.OK)
                    {
                        int nRead = 0;
                        byte[] buffer = new byte[2000];
                        Stream stream = dlgOpen.OpenFile();
                        MemoryStream mstream = new MemoryStream();
                        while ((nRead = stream.Read(buffer, 0, buffer.Length)) == buffer.Length)
                        {
                            mstream.Write(buffer, 0, buffer.Length);
                        }

                        if (nRead > 0)
                            mstream.Write(buffer, 0, nRead);

                        stream.Close();
                        X509Certificate2 cert;
                        bool isPFX = false;
                        string ext = dlgOpen.FileName.Substring(dlgOpen.FileName.LastIndexOf('.') + 1);
                        dlgOpen.Dispose();
                        if (ext.ToLower().Equals("pfx") || ext.ToLower().Equals("p12"))
                        {
                            isPFX = true;
                            PasswordForm pwdForm = new PasswordForm();
                            if (pwdForm.ShowDialog() == DialogResult.OK)
                            {
                                cert = new X509Certificate2(mstream.ToArray(), pwdForm.textBoxPassword.Text, X509KeyStorageFlags.Exportable);
                                pwdForm.Dispose();
                            }
                            else
                            {
                                pwdForm.Dispose();
                                return;
                            }
                        }
                        else
                        {
                            cert = new X509Certificate2(mstream.ToArray());
                        }

                        

                        CertKeyInfoForm infoDlg = new CertKeyInfoForm();
                        if (infoDlg.ShowDialog() == DialogResult.Cancel)
                        {
                            infoDlg.Dispose();
                            return;
                        }
                        if (!CurrentSession.IsLoggedIn && infoDlg.checkBoxPrivate.Checked)
                        {
                            if (MessageBox.Show("You cannot create a private object while you aren't logged in. Do you want to create a public object?", "Warning!", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                                infoDlg.checkBoxPrivate.Checked = false;
                            else
                            {
                                infoDlg.Dispose();
                                return;
                            }
                        }

                        if (isPFX)
                        {
                            if (!importKeyPair(cert, infoDlg.textBoxID.Text, infoDlg.textBoxLabel.Text, infoDlg.checkBoxPrivate.Checked, infoDlg.checkBoxModifiable.Checked))
                            {
                                infoDlg.Dispose();
                                return;
                            }
                        }

                        if (!importCertificate(cert, infoDlg.textBoxID.Text, infoDlg.textBoxLabel.Text, infoDlg.checkBoxModifiable.Checked))
                        {
                            infoDlg.Dispose();
                            return;
                        }

                        infoDlg.Dispose();

                        showMessage("File Imported succesfully!");

                        btnCertificate_Click(null, null);
                    }
                    else
                    {
                        dlgOpen.Dispose();
                    }
                }
                else if (_currentView == ViewState.Datas)
                {
                    dlgOpen.DefaultExt = "data";
                    dlgOpen.Filter = "Data File (*.data)|*.data|Data File (*.dat)|*.dat|All Files (*.*)|*.*";
                    if (dlgOpen.ShowDialog(this) == DialogResult.OK)
                    {
                        DataInfoForm dataDlg = new DataInfoForm();
                        if (dataDlg.ShowDialog() == DialogResult.Cancel)
                        {
                            dataDlg.Dispose();
                            return;
                        }

                        if (!CurrentSession.IsLoggedIn && dataDlg.checkBoxPrivate.Checked)
                        {
                            if (MessageBox.Show("You cannot create a private object while you aren't logged in. Do you want to create a public object?", "Warning!", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                                dataDlg.checkBoxPrivate.Checked = false;
                            else
                            {
                                dlgOpen.Dispose();
                                dataDlg.Dispose();
                                return;
                            }
                        }

                        int nRead = 0;
                        byte[] buffer = new byte[2000];
                        Stream stream = dlgOpen.OpenFile();
                        MemoryStream mstream = new MemoryStream();
                        while ((nRead = stream.Read(buffer, 0, buffer.Length)) == buffer.Length)
                        {
                            mstream.Write(buffer, 0, buffer.Length);
                        }

                        if (nRead > 0)
                            mstream.Write(buffer, 0, nRead);

                        stream.Close();

                        dataDlg.Dispose();
                        dlgOpen.Dispose();

                        CryptokiCollection template = new CryptokiCollection();
                        template.Add(new ObjectAttribute(ObjectAttribute.CKA_CLASS, CryptokiObject.CKO_DATA));
                        template.Add(new ObjectAttribute(ObjectAttribute.CKA_LABEL, dataDlg.textBoxLabel.Text));
                        template.Add(new ObjectAttribute(ObjectAttribute.CKA_APPLICATION, dataDlg.textBoxApp.Text));
                        template.Add(new ObjectAttribute(ObjectAttribute.CKA_TOKEN, true));
                        template.Add(new ObjectAttribute(ObjectAttribute.CKA_PRIVATE, dataDlg.checkBoxPrivate.Checked));
                        template.Add(new ObjectAttribute(ObjectAttribute.CKA_MODIFIABLE, dataDlg.checkBoxModifiable.Checked));
                        template.Add(new ObjectAttribute(ObjectAttribute.CKA_VALUE, mstream.ToArray()));

                        Data data = (Data)session.Objects.Create(template);

                        showMessage("Data imported succesfully!");

                        btnData_Click(null, null);
                    }
                    else
                    {
                        dlgOpen.Dispose();
                    }
                }
                else if (_currentView == ViewState.Keys)
                {
                    dlgOpen.DefaultExt = "pfx";
                    dlgOpen.Filter = "Personal Information Exchange File (*.pfx)|*.pfx|PKCS#12 File (*.p12)|*.p12|All Files (*.*)|*.*";
                    if (dlgOpen.ShowDialog(this) == DialogResult.OK)
                    {
                        int nRead = 0;
                        byte[] buffer = new byte[2000];
                        Stream stream = dlgOpen.OpenFile();
                        MemoryStream mstream = new MemoryStream();
                        while ((nRead = stream.Read(buffer, 0, buffer.Length)) == buffer.Length)
                        {
                            mstream.Write(buffer, 0, buffer.Length);
                        }

                        if (nRead > 0)
                            mstream.Write(buffer, 0, nRead);

                        stream.Close();
                        dlgOpen.Dispose();

                        X509Certificate2 cert;
                        PasswordForm pwdForm = new PasswordForm();
                        if (pwdForm.ShowDialog() == DialogResult.OK)
                        {
                            cert = new X509Certificate2(mstream.ToArray(), pwdForm.textBoxPassword.Text, X509KeyStorageFlags.Exportable);
                        }
                        else
                        {
                            pwdForm.Dispose();
                            return;
                        }

                        pwdForm.Dispose();

                        CertKeyInfoForm infoDlg = new CertKeyInfoForm();
                        if (infoDlg.ShowDialog() == DialogResult.Cancel)
                        {
                            infoDlg.Dispose();
                            return;
                        }
                        if (!CurrentSession.IsLoggedIn && infoDlg.checkBoxPrivate.Checked)
                        {
                            if (MessageBox.Show("You cannot create a private object while you aren't logged in. Do you want to create a public object?", "Warning!", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                                infoDlg.checkBoxPrivate.Checked = false;
                            else
                            {
                                infoDlg.Dispose();
                                return;
                            }
                        }

                        if (!importKeyPair(cert, infoDlg.textBoxID.Text, infoDlg.textBoxLabel.Text, infoDlg.checkBoxPrivate.Checked, infoDlg.checkBoxModifiable.Checked))
                        {
                            infoDlg.Dispose();
                            return;
                        }

                        if (!importCertificate(cert, infoDlg.textBoxID.Text, infoDlg.textBoxLabel.Text, infoDlg.checkBoxModifiable.Checked))
                        {
                            infoDlg.Dispose();
                            return;
                        }

                        infoDlg.Dispose();

                        showMessage("File Imported succesfully!");

                        btnKey_Click(null, null);
                    }
                }

                toolStripStatusLabel.Text = "Success";
                toolStripStatusLabel.Image = Properties.Resources.accept;
            }
            catch (CryptokiException ex)
            {
                toolStripStatusLabel.Text = ex.ErrorString;
                toolStripStatusLabel.Image = Properties.Resources.delete;
                showError(ex);
            }
            catch (Exception ex)
            {
                toolStripStatusLabel.Text = ex.Message;
                toolStripStatusLabel.Image = Properties.Resources.delete;
                showError(ex);
            }
        }

        private bool importCertificate(X509Certificate2 cert, string id, string label, bool modifiable)
        {            
            CryptokiCollection template = new CryptokiCollection();
            template.Add(new ObjectAttribute(ObjectAttribute.CKA_CLASS, CryptokiObject.CKO_CERTIFICATE));
            template.Add(new ObjectAttribute(ObjectAttribute.CKA_CERTIFICATE_TYPE, Certificate.CKC_X_509));
            template.Add(new ObjectAttribute(ObjectAttribute.CKA_SUBJECT, cert.SubjectName.RawData));
            template.Add(new ObjectAttribute(ObjectAttribute.CKA_ISSUER, cert.Issuer));
            template.Add(new ObjectAttribute(ObjectAttribute.CKA_SERIAL_NUMBER, cert.SerialNumber));
            template.Add(new ObjectAttribute(ObjectAttribute.CKA_ID, id));
            template.Add(new ObjectAttribute(ObjectAttribute.CKA_LABEL, label));                   
            template.Add(new ObjectAttribute(ObjectAttribute.CKA_TOKEN, true));
            template.Add(new ObjectAttribute(ObjectAttribute.CKA_VALUE, cert.RawData));
            template.Add(new ObjectAttribute(ObjectAttribute.CKA_MODIFIABLE, modifiable));
            
            CryptokiObject certificate = CurrentSession.Objects.Create(template);

            return true;
        }

        private bool importKeyPair(X509Certificate2 cert, string id, string label, bool priv, bool modifiable)
        {
            if (!cert.HasPrivateKey)
            {
                showError("Certificate doesn't have private key. Import failed!");
                return false;
            }           

            AsymmetricAlgorithm keyPair = cert.PrivateKey;
            
            if (keyPair is RSA)
            {
                RSAParameters keyParams = ((RSA)keyPair).ExportParameters(true);
                CryptokiCollection template = new CryptokiCollection();
                template.Add(new ObjectAttribute(ObjectAttribute.CKA_CLASS, CryptokiObject.CKO_PRIVATE_KEY));
                template.Add(new ObjectAttribute(ObjectAttribute.CKA_KEY_TYPE, Key.CKK_RSA));
                template.Add(new ObjectAttribute(ObjectAttribute.CKA_SUBJECT, cert.SubjectName.RawData));
                template.Add(new ObjectAttribute(ObjectAttribute.CKA_ID, id));
                template.Add(new ObjectAttribute(ObjectAttribute.CKA_LABEL, label));
                template.Add(new ObjectAttribute(ObjectAttribute.CKA_TOKEN, true));                
                template.Add(new ObjectAttribute(ObjectAttribute.CKA_MODULUS, keyParams.Modulus));
                template.Add(new ObjectAttribute(ObjectAttribute.CKA_PUBLIC_EXPONENT, keyParams.Exponent));
                template.Add(new ObjectAttribute(ObjectAttribute.CKA_PRIVATE_EXPONENT, keyParams.D));
                template.Add(new ObjectAttribute(ObjectAttribute.CKA_PRIVATE, priv));
                template.Add(new ObjectAttribute(ObjectAttribute.CKA_MODIFIABLE, modifiable));
                CryptokiObject priKey = CurrentSession.Objects.Create(template);
            }

            return true;

        }
        
        private void unblockToolStripMenuItem_Click(object sender, EventArgs e)
        {
            try
            {
                UnblockPINForm dlg = new UnblockPINForm();
                if (dlg.ShowDialog(this) == DialogResult.OK)
                {
                    Session session = CurrentSession;
                    if (session == null)
                    {
                        dlg.Dispose();
                        toolStripStatusLabel.Text = "Device removed";
                        toolStripStatusLabel.Image = Properties.Resources.delete;
                        showError(CryptokiException.GetErrorString(CryptokiException.CKR_DEVICE_REMOVED));
                        return;
                    }

                    if (session.IsLoggedIn)
                    {
                        session.Logout();
                        textBoxPIN.Enabled = true;
                        textBoxPIN.Text = "";
                        btnVerifyPIN.Enabled = true;
                        toolStripStatusLabel.Text = "";
                        toolStripStatusLabel.Image = null;
                        pictureBoxPIN.Image = Properties.Resources.delete;    
               
                    }

                    int nRet = session.Login(Session.CKU_SO, dlg.textBoxPUK.Text);
                    if (nRet != 0)
                    {
                        dlg.Dispose();
                        showError(CryptokiException.GetErrorString(nRet));
                        return;
                    }

                    nRet = session.InitPIN(dlg.textBoxNewPIN1.Text);
                    if (nRet != 0)
                        showError(CryptokiException.GetErrorString(nRet));
                    else
                        showMessage("PIN unblocked succesfully!");

                    dlg.Dispose();

                    session.Logout();

                    
                }

                toolStripStatusLabel.Text = "Success";
                toolStripStatusLabel.Image = Properties.Resources.accept;
            }
            catch (CryptokiException ex)
            {
                toolStripStatusLabel.Text = ex.ErrorString;
                toolStripStatusLabel.Image = Properties.Resources.delete;
                showError(ex);
            }
            catch (Exception ex)
            {
                toolStripStatusLabel.Text = ex.Message;
                toolStripStatusLabel.Image = Properties.Resources.delete;
                showError(ex);
            }
        }
        
        

        private void showToolsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.Enabled = false;
            textBoxPIN.Enabled = false;
            btnVerifyPIN.Enabled = false;
//            _cryptoki.Finalize(IntPtr.Zero);
            listViewContent.Items.Clear();
            Init();
            Show();
            WindowState = FormWindowState.Normal;
        }

        private void aboutToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            AboutForm f = new AboutForm();
            f.ShowDialog(this);
        }

        private void exitToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            ExitApp();
        }

        private void changePINToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            Init();
            changePINToolStripMenuItem_Click(sender, e);
        }

        private void unblockPINToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Init();
            unblockToolStripMenuItem_Click(sender, e);
        }

        private void MainForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            //if (e.CloseReason == CloseReason.UserClosing)
            //{
            //    Hide();
            //    WindowState = FormWindowState.Minimized;
            //    e.Cancel = true;                
            //}
            //else
            //{
                _bMonitorEnable = false;               
            //}

            if (_cryptoki != null)
            {
                _cryptoki.Finalize(IntPtr.Zero);                
            }
        }
      
        

        private void btnRefresh_MouseEnter(object sender, EventArgs e)
        {            
            btnRefresh.FlatAppearance.BorderSize = 1;          
        }

        private void btnRefresh_MouseLeave(object sender, EventArgs e)
        {
            btnRefresh.FlatAppearance.BorderSize = 0;
        }
       
        private void btnRefresh_Click(object sender, EventArgs e)
        {
            this.Cursor = Cursors.WaitCursor;
            _bMonitorEnable = false;
            if (_cryptoki != null)
            {
                _cryptoki.Finalize(IntPtr.Zero);
            }

            listViewContent.Items.Clear();

            _cryptoki = new Cryptoki(Properties.Settings.Default.CryptokiPath);

            Init();

            this.Cursor = Cursors.Default;
        }

        private void exitToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ExitApp();
        }

        private void pINToolStripMenuItem_DropDownOpening(object sender, EventArgs e)
        {
            changePINToolStripMenuItem.Enabled = CurrentSession != null;
            unblockToolStripMenuItem.Enabled = CurrentSession != null;
        }

        private void contextMenuStrip_Opening(object sender, CancelEventArgs e)
        {
            changePINToolStripMenuItem1.Enabled = CurrentSession != null;
            unblockPINToolStripMenuItem.Enabled = CurrentSession != null;
        }

        private void settingsToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            SettingsForm form = new SettingsForm();
            if (form.ShowDialog(this) == DialogResult.OK)
            {

                FileStream fs = new FileStream(Program.appDir + "\\conf.props", FileMode.Create);
                // TODO: FIX THIS. PROPS IS NOW REMOVED
                //props.Store(fs, "Cryptoki Browser conf");
                fs.Close();

                btnRefresh_Click(null, null);
            }

            form.Dispose();
        }

        private void contextMenuObject_Opening(object sender, CancelEventArgs e)
        {
            switch (_currentView)
            {
                case ViewState.Info:
                    contextMenuObject.Items[0].Visible = false;
                    contextMenuObject.Items[1].Visible = false;
                    contextMenuObject.Items[3].Visible = false;
                    contextMenuObject.Items[4].Visible = false;
                    contextMenuObject.Items[2].Visible = false;
                    if (listViewContent.SelectedItems.Count > 0)
                    {
                        if (listViewContent.SelectedItems[0].Tag is Slot)
                        {
                            contextMenuObject.Items[0].Visible = true;
                        }
                        else
                        {
                            e.Cancel = true;
                        }
                    }
                    else
                    {
                        e.Cancel = true;
                    }
                    break;

                case ViewState.Certs:
                    contextMenuObject.Items[0].Visible = listViewContent.SelectedItems.Count > 0;
                    contextMenuObject.Items[1].Visible = listViewContent.SelectedItems.Count > 0;
                    contextMenuObject.Items[2].Visible = true;
                    contextMenuObject.Items[3].Visible = listViewContent.SelectedItems.Count > 0;
                    contextMenuObject.Items[4].Visible = listViewContent.SelectedItems.Count > 0;

                    break;

                case ViewState.Datas:
                    contextMenuObject.Items[0].Visible = listViewContent.SelectedItems.Count > 0;
                    contextMenuObject.Items[1].Visible = listViewContent.SelectedItems.Count > 0;
                    contextMenuObject.Items[2].Visible = true;
                    contextMenuObject.Items[3].Visible = false;
                    contextMenuObject.Items[4].Visible = listViewContent.SelectedItems.Count > 0;
                    break;

                case ViewState.Keys:
                    contextMenuObject.Items[0].Visible = listViewContent.SelectedItems.Count > 0;
                    contextMenuObject.Items[1].Visible = listViewContent.SelectedItems.Count > 0;
                    contextMenuObject.Items[2].Visible = true;
                    contextMenuObject.Items[3].Visible = false;
                    contextMenuObject.Items[4].Visible = listViewContent.SelectedItems.Count > 0;
                    break;

                default:
                    e.Cancel = true;
                    break;
            }

        }

        private void toolStripMenuItemExplore_Click(object sender, EventArgs e)
        {
            Explore();
        }

        private void toolStripMenuItemExport_Click(object sender, EventArgs e)
        {
            btnExport_Click(sender, e);
        }

        private void toolStripMenuItemView_Click(object sender, EventArgs e)
        {
            byte[] rawcert = ((Certificate)listViewContent.SelectedItems[0].Tag).CertificateValue;
            ShowCertificate(rawcert);                                         
        }

        private void listViewContent_MouseClick(object sender, MouseEventArgs e)
        {
            //if (e.Button == MouseButtons.Right)
            //{
            //    contextMenuObject.Show(this.Left + listViewContent.Left + e.X, this.Top + listViewContent.Top + e.Y);
            //}
        }

        private void importToolStripMenuItem_Click(object sender, EventArgs e)
        {
            btnImport_Click(sender, e);
        }

        private void deleteToolStripMenuItem_Click(object sender, EventArgs e)
        {
            btnDelete_Click(sender, e);
        }
        


    }
}
