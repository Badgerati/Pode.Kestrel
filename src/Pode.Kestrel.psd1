#
# Module manifest for module 'Pode.Kestrel'
#
# Generated by: Matthew Kelly (Badgerati)
#
# Generated on: 07/10/2020
#

@{
    # Script module or binary module file associated with this manifest.
    RootModule = 'Pode.Kestrel.psm1'

    # Version number of this module.
    ModuleVersion = '1.1.0'

    # ID used to uniquely identify this module
    GUID = '4c6cae5a-8e62-48a2-b2e5-7511ffe2d438'

    # Author of this module
    Author = 'Matthew Kelly (Badgerati)'

    # Copyright statement for this module
    Copyright = 'Copyright (c) 2020 Matthew Kelly (Badgerati), licensed under the MIT License.'

    # Description of the functionality provided by this module
    Description = 'Kestrel engine for the Pode PowerShell web server'

    # Minimum version of the Windows PowerShell engine required by this module
    PowerShellVersion = '6.0'

    # Assemblies that must be loaded prior to importing this module
    RequiredAssemblies = @(
        'System.Web',
        'System.Net.Http',
        './Libs/Listener.dll'
    )

    # Private data to pass to the module specified in RootModule/ModuleToProcess. This may also contain a PSData hashtable with additional module metadata used by PowerShell.
    PrivateData = @{
        PSData = @{

            # Tags applied to this module. These help with module discovery in online galleries.
            Tags = @('powershell', 'web', 'server', 'pode', 'kestrel')

            # A URL to the license for this module.
            LicenseUri = 'https://raw.githubusercontent.com/Badgerati/Pode.Kestrel/master/LICENSE.txt'

            # A URL to the main website for this project.
            ProjectUri = 'https://github.com/Badgerati/Pode.Kestrel'

            # A URL to an icon representing this module.
            IconUri = 'https://cdn.rawgit.com/Badgerati/Pode.Kestrel/master/images/icon.png'

        }
    }
}