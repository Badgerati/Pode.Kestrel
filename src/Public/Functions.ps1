using namespace PodeKestrel

function New-PodeKestrelListener
{
    [CmdletBinding()]
    param(
        [Parameter(Mandatory=$true)]
        [System.Threading.CancellationToken]
        $CancellationToken,

        [Parameter(Mandatory=$true)]
        [PodeListenerType]
        $Type
    )

    return [PodeListener]::new($CancellationToken, $Type)
}

function New-PodeKestrelListenerSocket
{
    [CmdletBinding()]
    param(
        [Parameter(Mandatory=$true)]
        [ipaddress]
        $Address,

        [Parameter(Mandatory=$true)]
        [int]
        $Port,

        [Parameter()]
        [System.Security.Authentication.SslProtocols]
        $SslProtocols,

        [Parameter()]
        [X509Certificate]
        $Certificate,

        [Parameter()]
        [bool]
        $AllowClientCertificate
    )

    return [PodeSocket]::new($Address, $Port, $SslProtocols, $Certificate, $AllowClientCertificate)
}