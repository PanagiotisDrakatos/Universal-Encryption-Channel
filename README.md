<h1>Universal-Encryption-Channel</h1>
<p></p>
Universal Encryption Channel aims at providing an easy-to-use <b>Cross Platform</b> API which allow an encrypted  communication over sockets across server-client program. It provides the opportunity to client from Windows-Universal-Application  to interact with a server written in pure java and send and receive encrypted messages with the `DHE-RSA` key exchange
<p></p>

<h1>Diffie-Hellman Key Exchange</h1>

<p>This photo is made in <a href="https://products.office.com/el-gr/visio/flowchart-software">Microsoft Visio 2013</a>
and it describes the key exchanges over the Server-Client program. It describes exactly with details how 
both applications they begin to share keys and also how they try to ensure the model designed to guide policies for information security within an organization,the well known as <b>CIA(Confidentiality,Integrity,Availability)</b></p>

<h4>Confidentiality</h4>
<p>is roughly equivalent to <a href="https://en.wikipedia.org/wiki/Privacy">privacy</a>. Measures undertaken to ensure confidentiality are designed to prevent sensitive information from reaching the wrong people, while making sure that the right people can in fact get it: Access must be restricted to those authorized to view the data in question. It is common, as well, for data to be categorized according to the amount and type of damage that could be done should it fall into unintended hands.</p>

<h4>Integrity</h4> 

<p><a href="https://en.wikipedia.org/wiki/Integrity">Integrity</a> involves maintaining the consistency, accuracy, and trustworthiness of data over its entire life cycle. Data must not be changed in transit, and steps must be taken to ensure that data cannot be altered by unauthorized people (for example, in a breach of confidentiality). These measures include file permissions and user access controls. Version control maybe used to prevent erroneous changes or accidental deletion by authorized users becoming a problem</p>

<h4>Availability</h4> 

<p><a href="https://en.wikipedia.org/wiki/Availability">Availability</a> is best ensured by rigorously maintaining all hardware, performing hardware repairs immediately when needed and maintaining a correctly functioning operating system environment that is free of software conflicts. It’s also important to keep current with all necessary system upgrades.</p>

![alt tag](https://github.com/stathmarxis/Testiong-Repo/blob/master/DH_RSA.PNG)

<h1>How do I use this library with Gradle</h1>

<h2> Java requirements</h2>
<p>For now you get to build this yourself. Eventually, when it has enough testing, I might consider publishing it to some maven repository.</p>

<ol>
<li>Install local maven (seriously, if you don't do this, nothing else will work)</li>
<li>Clone this repo</li>
<li>Install the JDK and Netbeans <a href="http://www.oracle.com/technetwork/articles/javase/jdk-netbeans-jsp-142931.html"> Netbeans & JDK</a></li>
<li>Make sure Netbeans has Gradle Plugin else you can download from <a href="http://plugins.netbeans.org/plugin/44510/gradle-support"> here</a></li>
</ol>

<p>If you are going to use this library with Java then go to the 'java' sub-directory and run 'gradlew install'.</p>

<p>I would also recommend running 'gradlew test'. This will make sure you are properly set up and will copy your test files which I recommend reading.</p>

<p>Now everything should be build and installed into your local maven.</p>

<p>Now go to your build.gradle file (or equivalent) and make sure you add:</p>

```
apply plugin: 'maven'
```
---

<p>Then go to your repositories and add:</p>

<div class="highlight highlight-source-groovy"><pre>mavenLocal()</pre></div>

<p>Then go to dependencies and add in:</p>

```
dependencies {
    // TODO: Add dependencies here ...
    // You can read more about how to add dependency here:
    //   http://www.gradle.org/docs/current/userguide/dependency_management.html#sec:how_to_declare_your_dependencies
    testCompile group: 'junit', name: 'junit', version: '4.10'
    
    compile 'commons-codec:commons-codec:1.10'//commons-codec is an implementation of the Base64 encoder and Base64 decoder
    compile 'org.bouncycastle:bcprov-jdk16:1.46'//The Bouncy Castle APIs used for cryptography algorithms  for Java and C# 
    //just like  PKCS #7
    compile 'org.json:json:20151123'//store information in an organized, easy-to-access manner
    compile 'com.google.code.gson:gson:2.2.2'// serialize and deserialize Java json objects
}
```
---
<h3>Install the unlimited strength policy files</h3>
<p>The correct solution to run the Java server is that you must take consider to install <a href="http://www.oracle.com/technetwork/java/javase/downloads/jce8-download-2133166.html">unlimited strength policy files<a/>. While this is probably the right solution for your development, it quickly becomes a major hassle (if not a roadblock) to have non-technical users install the files on every computer. There is no way to distribute the files with your program. You must be installed in the JRE directory (which may even be read-only due to permissions) to run this application also you will have the opportunity to set your owns key size because for testing purposes the application does not provide big keys size.</p>

<h2>Universal Windows app requirements</h2>
<p><strong>Client:</strong> Windows 10</p>

<p><strong>Server:</strong> Windows Server 2016 Technical Preview</p>

<p><strong>Phone:</strong>  Windows 10</p>

<h2>Network capabilities</h2>
<p>SecureUWPChannel sample requires that network capabilities be set in the Package.appxmanifest file to allow the app to access the network at runtime. These capabilities can be set in the app manifest using Microsoft Visual Studio. For more information on network capabilities, see <a href="http://msdn.microsoft.com/library/windows/apps/hh770532">How to set network capabilities</a></p>

<h2><a id="user-content-build-the-sample" class="anchor" href="#build-the-sample" aria-hidden="true"><svg aria-hidden="true" class="octicon octicon-link" height="16" role="img" version="1.1" viewBox="0 0 16 16" width="16"><path d="M4 9h1v1h-1c-1.5 0-3-1.69-3-3.5s1.55-3.5 3-3.5h4c1.45 0 3 1.69 3 3.5 0 1.41-0.91 2.72-2 3.25v-1.16c0.58-0.45 1-1.27 1-2.09 0-1.28-1.02-2.5-2-2.5H4c-0.98 0-2 1.22-2 2.5s1 2.5 2 2.5z m9-3h-1v1h1c1 0 2 1.22 2 2.5s-1.02 2.5-2 2.5H9c-0.98 0-2-1.22-2-2.5 0-0.83 0.42-1.64 1-2.09v-1.16c-1.09 0.53-2 1.84-2 3.25 0 1.81 1.55 3.5 3 3.5h4c1.45 0 3-1.69 3-3.5s-1.5-3.5-3-3.5z"></path></svg></a>Build the sample</h2>

<ol>
<li>If you download the samples ZIP, be sure to unzip the entire archive, not just the folder with the sample you want to build. </li>
<li>Start Microsoft Visual Studio 2015 and select <strong>File</strong> &gt; <strong>Open</strong> &gt; <strong>Project/Solution</strong>.</li>
<li>Starting in the folder where you unzipped the sample, go to the SecureUWPChannel subfolder,and after this yoy are ready to double-click the Visual Studio 2015 Solution (.sln) file.</li>
<li>Press Ctrl+Shift+B, or select <strong>Build</strong> &gt; <strong>Build Solution</strong>.</li>
</ol>

<h2><a id="user-content-run-the-sample" class="anchor" href="#run-the-sample" aria-hidden="true"><svg aria-hidden="true" class="octicon octicon-link" height="16" role="img" version="1.1" viewBox="0 0 16 16" width="16"><path d="M4 9h1v1h-1c-1.5 0-3-1.69-3-3.5s1.55-3.5 3-3.5h4c1.45 0 3 1.69 3 3.5 0 1.41-0.91 2.72-2 3.25v-1.16c0.58-0.45 1-1.27 1-2.09 0-1.28-1.02-2.5-2-2.5H4c-0.98 0-2 1.22-2 2.5s1 2.5 2 2.5z m9-3h-1v1h1c1 0 2 1.22 2 2.5s-1.02 2.5-2 2.5H9c-0.98 0-2-1.22-2-2.5 0-0.83 0.42-1.64 1-2.09v-1.16c-1.09 0.53-2 1.84-2 3.25 0 1.81 1.55 3.5 3 3.5h4c1.45 0 3-1.69 3-3.5s-1.5-3.5-3-3.5z"></path></svg></a>Run the sample</h2>

<p>The next steps depend on whether you just want to deploy the sample or you want to both deploy and run it.</p>

<h3><a id="user-content-deploying-the-sample" class="anchor" href="#deploying-the-sample" aria-hidden="true"><svg aria-hidden="true" class="octicon octicon-link" height="16" role="img" version="1.1" viewBox="0 0 16 16" width="16"><path d="M4 9h1v1h-1c-1.5 0-3-1.69-3-3.5s1.55-3.5 3-3.5h4c1.45 0 3 1.69 3 3.5 0 1.41-0.91 2.72-2 3.25v-1.16c0.58-0.45 1-1.27 1-2.09 0-1.28-1.02-2.5-2-2.5H4c-0.98 0-2 1.22-2 2.5s1 2.5 2 2.5z m9-3h-1v1h1c1 0 2 1.22 2 2.5s-1.02 2.5-2 2.5H9c-0.98 0-2-1.22-2-2.5 0-0.83 0.42-1.64 1-2.09v-1.16c-1.09 0.53-2 1.84-2 3.25 0 1.81 1.55 3.5 3 3.5h4c1.45 0 3-1.69 3-3.5s-1.5-3.5-3-3.5z"></path></svg></a>Deploying the sample</h3>

<ul>
<li>Select <strong>Build</strong> &gt; <strong>Deploy Solution</strong>. </li>
</ul>

<h3><a id="user-content-deploying-and-running-the-sample-on-a-windows-10-desktop" class="anchor" href="#deploying-and-running-the-sample-on-a-windows-10-desktop" aria-hidden="true"><svg aria-hidden="true" class="octicon octicon-link" height="16" role="img" version="1.1" viewBox="0 0 16 16" width="16"><path d="M4 9h1v1h-1c-1.5 0-3-1.69-3-3.5s1.55-3.5 3-3.5h4c1.45 0 3 1.69 3 3.5 0 1.41-0.91 2.72-2 3.25v-1.16c0.58-0.45 1-1.27 1-2.09 0-1.28-1.02-2.5-2-2.5H4c-0.98 0-2 1.22-2 2.5s1 2.5 2 2.5z m9-3h-1v1h1c1 0 2 1.22 2 2.5s-1.02 2.5-2 2.5H9c-0.98 0-2-1.22-2-2.5 0-0.83 0.42-1.64 1-2.09v-1.16c-1.09 0.53-2 1.84-2 3.25 0 1.81 1.55 3.5 3 3.5h4c1.45 0 3-1.69 3-3.5s-1.5-3.5-3-3.5z"></path></svg></a>Deploying and running the sample on a Windows 10 Desktop</h3>

<ul>
<li>To debug the sample and then run it, press F5 or use <strong>Debug</strong> &gt; <strong>Start Debugging</strong>. To run the sample without debugging, press Ctrl+F5 or use <strong>Debug</strong> &gt; <strong>Start Without Debugging</strong>.</li>
</ul>

<h2>Deploying and running the sample on a Windows 10 Phone</h2>

<p>this is not available on Windows Phone. basically this api is not tested in windows phone yet very soon i will publish and new version which it will support windows 10 phones stay tuned in the repository and don't forget to contribute with a simple click Fork</p>

<h2>Windows 10 IoT Core</h2>
<p>This api Platform  have not been validated on Windows IoT Core very soon i will publish and new version which it will support Raspberry Pi 2 stay tuned in the repository</p>
<ul>
<li><p><a href="http://go.microsoft.com/fwlink/?LinkId=691711">Raspberry Pi 2</a></p></li>
</ul>

<p>For more information about Windows 10 IoT Core, see  online documentation <a href="http://windowsondevices.com">here</a></p>

<p>Please download, build, deploy, and contribute!! For more information and descriptions about this sample  you should feel free to create an issue and ask question and i will reply in my earliest convenience and don't forget to Fork the repository and upload whatever you want tha it will be helpful for this project.</p>

