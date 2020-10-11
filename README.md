# Pode.Kestrel

This is the Kestrel Listener for the [Pode](https://github.com/Badgerati/Pode) PowerShell web server. The Kestrel listener, at present, only support HTTP/HTTPS.

> This listener only works with Pode 2.0+ and PowerShell 6.0+


## Usage

To begin using the Kestrel listener, you'll first need to install the module:

```powershell
Install-Module -Name Pode.Kestrel
```

then, in your main server script, you'll need to import the module and set the `-ListenerType`:

```powershell
Import-Module -Name Pode.Kestrel

Start-PodeServer -ListenerType Kestrel {
    # endpoints, routes, etc
}
```

and that's it!
