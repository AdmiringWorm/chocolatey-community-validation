namespace Chocolatey.CCR.Tests.Rules
{
    using System.Threading.Tasks;
    using Chocolatey.CCR.Rules;
    using NUnit.Framework;

    [Category("Requirements")]
    public class NuspecContainsEmailsRuleTests : RuleTestBase<NuspecContainsEmailsRule>
    {

        [Test]
        public async Task ShouldFlagAllEmailsInNuspec([Values] bool outputEmails)
        {
            NuspecContainsEmailsRule.OutputFoundEmails = outputEmails;

            const string testContent = @"<?xml version=""1.0"" encoding=""utf-8""?>
<package xmlns=""http://schemas.microsoft.com/packaging/2015/06/nuspec.xsd"">
  <metadata>
    <id>bobbbbb@te.st</id>
    <version>abc@def.gh</version>
    <packageSourceUrl>email:package-source@test.com</packageSourceUrl>
    <owners>owner1, owner@my-email.com</owners>
    <title>(Bob@test.co.uk)</title>
    <authors>Chocolatey,bob@bob.co.uk</authors>
    <projectUrl>https://project@test.nz</projectUrl>
    <iconUrl>icon@bob.com</iconUrl>
    <copyright>bob@bob.co.uk</copyright>
    <licenseUrl>bob@test.co.uk</licenseUrl>
    <requireLicenseAcceptance>true</requireLicenseAcceptance>
    <projectSourceUrl>pkg-source@test.no</projectSourceUrl>
    <docsUrl>bob@bob2.co.uk</docsUrl>
    <mailingListUrl>https://bob@bob.co.uk</mailingListUrl>
    <bugTrackerUrl>bob@bob.co.uk</bugTrackerUrl>
    <tags>software with email bobby@bob.no and space delimited</tags>
    <summary>software is provided by BOB@test.com</summary>
    <description>My awesome description provided by bob@bob.co.uk, lezy@workplace.com and mailto:someone@worker.no.</description>
    <releaseNotes>My awesome release notes provided by awesome@user.com</releaseNotes>
    <language>bob@bob.co.uk</language>

    <provides>pkg-name, provides@test.com</provides>
    <conflicts>bob+label@bob.co.uk,pkg-name</conflicts>
    <replaces>pkg-name,bobby@bobtastic.ca</replaces>
  </metadata>
</package>";

            await VerifyNuspec(testContent);
        }
        [Test]
        public async Task ShouldFlagEmailUsedOutsideOfMetadataFields()
        {
            const string testContent = @"<package xmlns=""http://schemas.microsoft.com/packaging/2015/06/nuspec.xsd"">
<metadata />
bobtastic@testinng.com
</package>";

            await VerifyNuspec(testContent);
        }

        [Test]
        public async Task ShouldNotFlagFullNuspecWithoutTemplateValues()
        {
            const string testContent = @"<?xml version=""1.0"" encoding=""utf-8""?>
<!-- Do not remove this test for UTF-8: if “Ω” doesn’t appear as greek uppercase omega letter enclosed in quotation marks, you should use an editor that supports UTF-8, not this one. -->
<package xmlns=""http://schemas.microsoft.com/packaging/2015/06/nuspec.xsd"">
  <metadata>
    <id>blender</id>
    <version>3.6.4</version>
    <title>Blender</title>
    <owners>chocolatey-community, Redsandro</owners>
    <authors>Blender Foundation</authors>
    <licenseUrl>https://www.blender.org/about/license/</licenseUrl>
    <projectUrl>https://www.blender.org</projectUrl>
    <projectSourceUrl>https://git.blender.org/gitweb/</projectSourceUrl>
    <bugTrackerUrl>https://developer.blender.org/maniphest/</bugTrackerUrl>
    <mailingListUrl>https://wiki.blender.org/wiki/Communication/Contact#Mailing_Lists</mailingListUrl>
    <iconUrl>https://cdn.jsdelivr.net/gh/chocolatey-community/chocolatey-packages@edba4a5849ff756e767cba86641bea97ff5721fe/icons/blender.svg</iconUrl>
    <requireLicenseAcceptance>false</requireLicenseAcceptance>
    <!-- Do not touch the description here in the nuspec file. Description is imported during update from the Readme.md file -->
    <description><![CDATA[
Blender is a free and open-source professional-grade 3D computer graphics and video compositing program.

### Realistic Materials

![Realistic Materials](https://i.imgur.com/AywgUj3.png)

### Fluid Simulations

![Fluid Simulations](https://i.imgur.com/ILwViaO.png)

## Features

* Blender is a fully integrated 3D content creation suite, offering a broad range of essential tools:
* Modeling
* Rendering
* Animation
* Video Editing and Compositing
* Texturing
* Rigging
* Simulations
* Game Creation.
* Cross platform, with an OpenGL GUI that is uniform on all major platforms (and customizable with Python scripts).
* High-quality 3D architecture enabling fast and efficient creation work-flow.
* Excellent community support from forums and IRC.
* Small executable size, optionally portable.

### Notes

- If you have multiple installations of the Blender software, the uninstallation of the Chocolatey package will likely fail. Please manually uninstall the Blender software (through 'Add and Remove Programs' or 'Programs and Features') and then run `choco uninstall blender` to remove the package.
- **If the package is out of date please check [Version History](#versionhistory) for the latest submitted version. If you have a question, please ask it in [Chocolatey Community Package Discussions](https://github.com/chocolatey-community/chocolatey-packages/discussions) or raise an issue on the [Chocolatey Community Packages Repository](https://github.com/chocolatey-community/chocolatey-packages/issues) if you have problems with the package. Disqus comments will generally not be responded to.**
]]></description>
    <summary>Blender is 3D creation for everyone, free to use for any purpose.</summary>
    <releaseNotes>https://wiki.blender.org/wiki/Reference/Release_Notes</releaseNotes>
    <copyright>Blender Foundation</copyright>
    <tags>blender 3d rendering foss cross-platform modeling animation admin</tags>
    <dependencies>
      <dependency id=""vcredist2008"" version=""9.0.30729.6161"" />
      <dependency id=""chocolatey-core.extension"" version=""1.3.3"" />
      <!--We could set chocolatey to version 0.10.4, but that version was broken so we use 0.10.5-->
      <dependency id=""chocolatey"" version=""0.10.5"" />
    </dependencies>
    <packageSourceUrl>https://github.com/chocolatey-community/chocolatey-packages/tree/master/automatic/blender</packageSourceUrl>
  </metadata>
  <files>
    <file src=""tools\**"" target=""tools"" />
  </files>
</package>";

            await VerifyEmptyResults(testContent);
        }
    }
}
