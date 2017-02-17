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
        var attributes = doc.Root.Attributes();
        foreach (var attribute in attributes) {
            if (attribute.Name.LocalName == "versionCode") {
                info.VersionCode = attribute.Value.ToString();
            } else if (attribute.Name.LocalName == "versionName") {
                info.VersionName = attribute.Value.ToString();
            } else if (attribute.Name.LocalName == "package") {
                info.PackgeName = attribute.Value.ToString();
            }
        }

        // find application element
        var app = doc.Root.Elements().Single((elm) => {
            return elm.Name.LocalName == "application";
        });
        // find launch activity

        var activity = app.Elements().Single((elm) => {
            if (elm.Name.LocalName == "activity") {
                var intent_filter = elm.Elements().Single((intent)=> {
                    
                    if (intent.Name.LocalName == "intent-filter") {
                        // 1. find android.intent.action.MAIN
                        var intent_action_main = intent.Elements().Any((intent_elm) => {
                            if (intent_elm.Name.LocalName == "action") {
                                var actionMain = intent_elm.Attributes().Single((intent_elm_att) => {
                                    if (intent_elm_att.Name.LocalName == "name") {
                                        if (intent_elm_att.Value.ToString() == "android.intent.action.MAIN") {
                                            return true;
                                        }
                                    }
                                    return false;
                                });
                                if (actionMain != null) return true;
                            }
                            return false;
                        });
                        // 2.find android.intent.category.LAUNCHER
                        var intent_launcher = intent.Elements().Any((intent_elm) => {
                            if (intent_elm.Name.LocalName == "category") {
                                var actionMain = intent_elm.Attributes().Single((intent_elm_att) => {
                                    if (intent_elm_att.Name.LocalName == "name") {
                                        if (intent_elm_att.Value.ToString() == "android.intent.category.LAUNCHER") {
                                            return true;
                                        }
                                    }
                                    return false;
                                });
                                if (actionMain != null) return true;
                            }
                            return false;
                        });

                        if (intent_action_main  && intent_launcher ) return true;
                    }

                    return false;
                });

                if (intent_filter != null) {
                    return true;
                }
            }
            return false;
        });

         
        return info;
    }
}