using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using ICSharpCode.SharpZipLib.Zip;
using AndroidXml;
using System.Xml.Linq;

class APKInfo
{
    public string PackgeName;
    public string VersionName;
    public string VersionCode;
    public string LauncherActivity;

    public static APKInfo ParseAPK(string apkFilePath)
    {
        APKInfo info = new APKInfo();
        ZipFile apkFile = new ZipFile(apkFilePath);
        var manifestEntry = apkFile.GetEntry("AndroidManifest.xml");
        var manifestInputStream = apkFile.GetInputStream(manifestEntry);
        var bytes = new byte[manifestEntry.Size];
        manifestInputStream.Read(bytes, 0, (int)manifestEntry.Size);
        var ms = new MemoryStream(bytes);
        var reader = new AndroidXmlReader(ms);
        XDocument doc = XDocument.Load(reader);

        // get packge info
        info.VersionCode = doc.Root.FindAttribute("versionCode").Value.ToString();
        info.VersionName = doc.Root.FindAttribute("versionName").Value.ToString();
        info.PackgeName = doc.Root.FindAttribute("package").Value.ToString();

        var application = doc.Root.FindSingleElement("application");

        var activities = application.FindElements("activity");

        var launchActivity = activities.Single((activity)=> {

            var intent_filter = activity.FindSingleElement("intent-filter");
            if (intent_filter == null) return false;

            // 是否存在 <action p1:name="android.intent.action.MAIN"></action>

            bool hasActionMain = intent_filter.FindElements("action").Any((ele)=> {
                string actionName = ele.FindAttribute("name").Value.ToString();
                return actionName == "android.intent.action.MAIN";
            });

            // 是否存在 <category p1:name="android.intent.category.LAUNCHER"></category>
            bool hasLauncher = intent_filter.FindElements("category").Any((ele) => {
                string actionName = ele.FindAttribute("name").Value.ToString();
                return actionName == "android.intent.category.LAUNCHER";
            });

            return hasActionMain && hasLauncher;

        });

        info.LauncherActivity = launchActivity.FindAttribute("name").Value.ToString();
        
        return info;
    }
}


                                                                                                                           