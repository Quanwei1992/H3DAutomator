using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

public static class XElementEx
{
    public static XAttribute FindAttribute(this XElement ele, string localName, string namespaceName = null)
    {
        var ret = ele.Attributes().Single((att) => {
            if (namespaceName != null) {
                return att.Name.LocalName == localName && att.Name.NamespaceName == namespaceName;
            } else {
                return att.Name.LocalName == localName;
            }
        });
        return ret;
    }


    public static XElement FindSingleElement(this XElement ele, string name,string namespaceName = null)
    {

        List<XElement> rets = new List<XElement>();
        var elements = ele.Elements();
        foreach (var element in elements) {
            if (namespaceName != null) {
                if (element.Name.LocalName == name && element.Name.NamespaceName == namespaceName) {
                    return element;
                }
            } else {
                if (element.Name.LocalName == name) {
                    return element;
                }
            }
        }
        return null;
    }

    public static XElement[] FindElements(this XElement ele, string name, string namespaceName = null)
    {
        List<XElement> rets = new List<XElement>();
        var elements = ele.Elements();
        foreach (var element in elements) {
            if (namespaceName != null) {
                if (element.Name.LocalName == name && element.Name.NamespaceName == namespaceName) {
                    rets.Add(element);
                }
            } else {
                if (element.Name.LocalName == name) {
                    rets.Add(element);
                }
            }
        }

        return rets.ToArray();
    }
}
