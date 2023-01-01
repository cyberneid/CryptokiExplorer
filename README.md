# CryptokiExplorer

## Objective

CryptokiExplorer is ........

## Middleware

First of all, to use CryptokiExplorer, we have to select a middleware.
There are a lot of compatible middlewares, like:

- CIE Middleware (we'll use this one)
- OpenSC

When CryptokiExplorer is launched for the first time a dialog appears asking for a Middleware path (the .dll). In our case, we'll use CIE Middlware.

### What's CIE Middleware

[CIE Middleware](https://www.cartaidentita.interno.gov.it/fornitori-di-servizi/documentaziosne-middleware-cie/) is the middleware created by Italian Government to read the Electronic Identity Card.
When installed, it'll create a .dll named CIEPKI.dll in System32.
For example, in our case, it's located in "C:/System32/CIEPKI.dll".

## The App
