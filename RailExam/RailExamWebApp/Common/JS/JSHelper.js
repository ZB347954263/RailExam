// Javascript for debug most
//DOM Helper Function
function getProperties(obj)
{	
    var ps;
    if(typeof(obj) != "object")
    {
        ps = "[" + obj + "]<BR/>";
        
        return ps;
    }
    
    try
    {
        for(var p in obj)
        {
            if(typeof(p) != "object")
            {					
	            ps += "[" + p + "=" + obj[p] + "]<BR/>";
            }
            else
            {
	            ps += "[" + p + "=" + getProperties(p) + "]<BR/>";
            }
        }
    }
    catch(e)
    {
        ps = "Not DOM Objects！" + getProperties(e);
    }
    
    return ps;			
}

function showProperties(obj)
{
	var win = window.open();
	win.title = "Properties of " + obj;
	win.document.write(getProperties(obj));
	win.document.close();
}

function getElementText(doc, xpath) 
{
    var retval = ""; 
    if(!doc) return ""; 
    
    var v = doc.selectSingleNode(xpath); 
    if(v) retval = v.text; 
    
    return retval; 
}

function getCookie(name) 
{
    var r = new RegExp("(^|;|\s)*" + name + "=([^;]*)(;|$)"); 
    var m = document.cookie.match(r); 
    
    return(!m ? "" : m[2]); 
}

function setCookie(name, value) 
{
    document.cookie = name + "=" + value; // + "; path=path; domain=domain name"; 
}

function deleteCookie(name) 
{
    document.cookie = name + "=; expires=" + (new Date(0)).toGMTString(); // + "; path=path; domain=domain name"; 
}
   
function removeElement(element) 
{
    if(element) 
    {
        element.parentNode.removeChild(element); 
    }
}

function replaceElement(desElement, srcElement) 
{
    if(srcElement) 
    {
        srcElement.parentNode.replaceChild(desElement, srcElement); 
    }
}

function getParameter(name) 
{
    var r = new RegExp("(\\?|#|&)" + name + "=([^&]*)(&|$)")
    var m = location.href.match(r)
    
    if(!m || m == "") m = top.location.href.match(r)
    
    return(!m ? "" : m[2]); 
}

function printfDatetime(st) 
{
    return st.replace(/^(?:\d{4}\-)|(?::\d{1,2})$/g, ""); 
}

function changeBodyScrollHeight() 
{
    top.document.body.style.overflowX = 'hidden'; 
    if(top.document.body.clientHeight < 505)
        top.document.body.style.overflowY = 'auto'; 
    else 
    {
        top.document.body.style.overflowY = 'hidden'; 
        top.document.body.scrollTop = 0; 
    }
    
    top.document.body.onscroll = function(){
        WinHeight = top.document.body.clientHeight - 30; 
    }
}

function truncateString(src, l, n) 
{
    var s = src.toInputField(); 
    
    if(isNaN(l)) 
    return ""; 
    
    if(!n) n = 0; 
    if(s.getRealLength() <= l) 
        return s.replace(/&/g, "&amp;").replace(/"/g, "&quot;"); 
    else 
    {
        var length = 0; 
        var idx = 0; 
        var tmpStr = []; 
        
        while((length < l - 1) && (idx < s.length)) 
        {
            if(/[\x00-\xFF]/.test(s.charAt(idx))) 
            {
                tmpStr.push(s.charAt(idx)); 
                length++; 
            }
            else if(length % 2 == 0) 
            {
                tmpStr.push(s.charAt(idx)); 
                length += 2; 
            }
            idx++; 
        }
        
        return tmpStr.join("").replace(/&/g, "&amp;").replace(/"/g, "&quot;") + (new Array(n + 1)).join("."); 
    }
}

String.prototype.trueEntityReplace = function() 
{
    return this.replace(/&#(\d+);?/g, 
        function(a, b) 
        {
            return String.fromCharCode(b)
        }); 
}

String.prototype.URIencode = function() 
{
    return this.replace(/[\x09\x0A\x0D\x21-\x29\x2B\x2C\x2F\x3A-\x3F\x5B-\x5E\x60\x7B-\x7E]/g, 
        function(a) {
            return "%" + ((a.charCodeAt(0) < 16) ? ("0" + a.charCodeAt(0).toString(16)) : (a.charCodeAt(0).toString(16)))}
        ).replace(/[\x00-\x20]/g, "+"); 
}

String.prototype.trim = function() 
{
    return this.replace(/^\s+|\s+$/g, ""); 
}

String.prototype.toInnerHTML = function() 
{
   var tmp = this; 
   tmp = tmp.replace(/^\s+|\s+$/g, "").replace(/</, "&lt;").replace(/>/g, "&gt;").replace(/ /g, "&nbsp;"); 
   
   if(window.ActiveXObject) return tmp.replace(/&apos;/g, "\'"); 
   else return tmp; 
}

String.prototype.toInputField = function() 
{
    return this.replace(/^\s+|\s+$/g, "").replace(/"/g, "\"").replace(/</g, "<").replace(/>/g, ">").replace(/&apos;/g, "\'").replace(/&/g, "&"); 
}

String.prototype.getRealLength = function() 
{
    return this.replace(/[^\x00-\xff]/g, "aa").length; 
}

function adjustSize(obj, w, h) 
{
    var w0 = obj.width; 
    var h0 = obj.height; 
    if(w0 < 1) 
    {
        var i = new Image();
         
        i.src = obj.src; 
        w0 = i.width; 
        h0 = i.height; 
    }
    
    if((w0 / h0) > (w / h)) 
    {
        if(w0 > w)obj.width = Math.abs(w); 
    }
    else 
    {
        if(h0 > h)obj.height = Math.abs(h); 
    }
    
    obj.onload = null; 
}

function fixSize(obj, w, h) 
{
    var i = new Image(); 
    i.src = obj.src; 
    w0 = i.width; 
    h0 = i.height; 
    if((w0 / h0) > (w / h)) 
    {
        obj.width = Math.abs(w); 
        obj.height = obj.width * h0 / w0; 
    }
    else 
    {
        obj.height = Math.abs(h); 
        obj.width = obj.height * w0 / h0; 
    }
    
    obj.onload = null; 
    i = null; 
}

// Hidden fields
function showHiddenFields(document)
{
    var inputs = document.getElementsByTagName("input");
    var win = window.open("Properties of HiddenFields:");
    var res = "";

    for(var i=0; i<inputs.length; i++)
    {
        if(inputs[i]["type"] && inputs[i]["type"] == "hidden")
        {
            res += "[id=" + inputs[i]["id"] 
                + ", <br/>name=" + inputs[i]["name"] 
                + ", <br/>value=" + inputs[i]["value"] 
                + "]<p/>";
        }
    }
    win.document.write(res);
    win.document.close();		
}

// Validators 
function validatePostCode(postCode)
{
    var RegExp = /[1-9]\d{5}/;
    
    return (RegExp.test(postCode));
}

function validatePhoneNumber(phoneNumber)
{
    var RegExp = /(\d{3})-?[1-9]\d{7}/;
    
    return (RegExp.test(phoneNumber));
}

function validateWebSite(webSite)
{
    var RegExp = /\w+([-+.]\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*/;
    
    return (RegExp.test(webSite));
}

function validateEmail(email)
{
    var RegExp = /\w+([-+.]\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*/;
    
    return (RegExp.test(email));
} 

function validateScore(score)
{
    var RegExp = /\d{1,2}.?(\d{1,2})?/;
    
    return (RegExp.test(score));
}

function validateTime(time)
{
    var RegExp = /\d{2}:\d{2}:\d{2}/;
    
    return (RegExp.test(time));
}

function validateDate(date)
{
    var RegExp = /[\d{2}|\d{4}]-\d{1,2}-\d{1,2}(\S\d{1,2}:\d{1,2}:\d{1,2})?/;
    
    return (RegExp.test(date));
}

function validateEmpty(text)
{
    if(text.length == 0)
    {
        return true;
    }
    var RegExp = /(\r)?([\t\s])*(\n)?/;
    
    return (RegExp.test(text));
}

// Elements
function $F(objId)
{
    return document.getElementById(objId);   
}

// Browser
var Browser = {}; 
Browser.isMozilla = (typeof document.implementation != 'undefined') 
    && (typeof document.implementation.createDocument != 'undefined') 
    && (typeof HTMLDocument != 'undefined'); 
Browser.isIE = window.ActiveXObject ? true : false; 
Browser.isFirefox = (navigator.userAgent.toLowerCase().indexOf("firefox") !=- 1); 
Browser.isSafari = (navigator.userAgent.toLowerCase().indexOf("safari") !=- 1); 
Browser.isOpera = (navigator.userAgent.toLowerCase().indexOf("opera") !=- 1); 

// XML
var e; 
if(Browser.isFirefox) {
    XMLDocument.prototype.selectSingleNode = Element.prototype.selectSingleNode = function(xpath) {
        var x = this.selectNodes(xpath)
        
        if(!x || x.length < 1)
            return null;
        
        return x[0]; 
    }
    
    XMLDocument.prototype.selectNodes = Element.prototype.selectNodes = function(xpath) {
        var xpe = new XPathEvaluator(); 
        var nsResolver = xpe.createNSResolver(this.ownerDocument == null ? this.documentElement : this.ownerDocument.documentElement); 
        var result = xpe.evaluate(xpath, this, nsResolver, 0, null); 
        var found = []; 
        var res; 

        while(res = result.iterateNext()) 
            found.push(res); 
            
        return found; 
    }
    
    XMLDocument.prototype.getOuterXML = Element.prototype.getOuterXML = function() {
        try {
            return new XMLSerializer().serializeToString(this); 
        }
        catch(e) {
            var d = document.createElement("div"); 
            d.appendChild(this.cloneNode(true)); 
            
            return d.innerHTML; 
        }
    }
    
    XMLDocument.prototype.__proto__.__defineGetter__("xml", function() {
        try {
            return new XMLSerializer().serializeToString(this); }
        catch(ex) {
            var d = document.createElement("div"); d.appendChild(this.cloneNode(true)); return d.innerHTML; 
        }
    }); 
    
    Element.prototype.__proto__.__defineGetter__("xml", function() {
        try {
            return new XMLSerializer().serializeToString(this); }
        catch(ex) {
            var d = document.createElement("div"); d.appendChild(this.cloneNode(true)); return d.innerHTML; 
        }
    }); 
      
    XMLDocument.prototype.__proto__.__defineGetter__("text", function() {
        return this.firstChild.textContent}); 
  
    Element.prototype.__proto__.__defineGetter__("text", function() {
        return this.textContent
    }); 
}
   
function parseXML(st) 
{
    var result = null; 
    
    if(Browser.isIE) 
    {
        result = getXMLDOM();
         
        if(result)
            result.loadXML(st); 
    }
    else 
    {
        var parser = new DOMParser(); 
        result = parser.parseFromString(st, "text/xml"); 
    }
    
    return result; 
}
   
function getXMLDOM() 
{
    if(!Browser.isIE)
        return null; 
        
    var xmldomversions = ['MSXML2.DOMDocument.5.0', 'MSXML2.DOMDocument.4.0', 'MSXML2.DOMDocument.3.0', 
        'MSXML2.DOMDocument', 'Microsoft.XMLDOM']; 
    for(var i = xmldomversions.length - 1; i >= 0; i--)
    {
        try 
        {
            return new ActiveXObject(xmldomversions[i]); 
        }
        catch(e) 
        {
        
        }
    }
    
    return document.createElement("XML"); 
}
   
function getXMLHTTP() 
{
    if(window.XMLHttpRequest)
    {
        try 
        {
            return new XMLHttpRequest(); 
        }
        catch(e) 
        {
        }
    }
    
    if(Browser.isIE) 
    {
        var xmlhttpversions = ['MSXML2.XMLHTTP.5.0', 'MSXML2.XMLHTTP.4.0', 
        'MSXML2.XMLHTTP.3.0', 'MSXML2.XMLHTTP', 'Microsoft.XMLHTTP']; 
        for(var i = xmlhttpversions.length - 1; i >= 0; i--)
        {
            try 
            {
                return new ActiveXObject(xmlhttpversions[i]); 
            }
            catch(e) 
            {
            
            }
        }
        
        var s = "对不起，您浏览器设置不支持异步请求，"
            + "请尝试在IE菜单中打开\n“工具”-“Internet选项”-“安全”-“自定义级别”，"
            + "将\n“对标记为可安全执行脚本的ActiveX控件执行脚本”和\n“运行ActiveX控件和插件”\n这两项选项更改为“允许”。"; 
        if(document.cookie.indexOf("xmlhttp_fail") >- 1)
            alert(s); status = s; 
        
        for(var i = 1; i < 32; i++)
            setTimeout("status=\"" + s.substring(0, 123).replace(/\n/g, "").substr(i) + "\"", i * 300 + 3000); 
            
        document.cookie = "xmlhttp_fail=prompted"; 
        
        return null; 
    }
}
