using namespace PodeKestrel

function New-PodeKestrelListener
{
    [CmdletBinding()]
    param(
        [Parameter(Mandatory=$true)]
        [System.Threading.CancellationToken]
        $CancellationToken,

        [Parameter()]
        $Type
    )

    return [PodeListener]::new($CancellationToken)
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
        $Type,

        [Parameter()]
        [X509Certificate]
        $Certificate,

        [Parameter()]
        [bool]
        $AllowClientCertificate
    )

    return [PodeSocket]::new($Address, $Port, $SslProtocols, $Certificate, $AllowClientCertificate)
}