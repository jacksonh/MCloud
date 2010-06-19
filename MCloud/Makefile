
SOURCES=Entity.cs  			\
	Node.cs  			\
	NodeAuth.cs			\
	NodeAuthType.cs			\
	NodeDriver.cs  			\
	NodeImage.cs  			\
	NodeLocation.cs 		\
	NodeOptions.cs			\
	NodeProvider.cs			\
	NodeSize.cs  			\
	NodeState.cs			\
	MCloud.Deploy/Deployment.cs			\
	MCloud.Deploy/MultiStepDeployment.cs		\
	MCloud.Deploy/PutDirectoryDeployment.cs	\
	MCloud.Deploy/PutFileDeployment.cs		\
	MCloud.Deploy/PutFilesDeployment.cs		\
	MCloud.Deploy/ScriptDeployment.cs		\
	MCloud.Deploy/SSHDeployment.cs		\
	MCloud.Deploy/SSHKeyDeployment.cs		\
	MCloud.EC2/EC2Driver.cs		\
	MCloud.EC2/EC2Node.cs			\
	MCloud.EC2/EC2NodeOptions.cs		\
	MCloud.EC2/EC2NodeSizes.cs		\
	MCloud.Linode/LinodeAPI.cs		\
	MCloud.Linode/LinodeCertificatePolicy.cs	\
	MCloud.Linode/LinodeDriver.cs		\
	MCloud.Linode/LinodeError.cs		\
	MCloud.Linode/LinodeNode.cs		\
	MCloud.Linode/LinodeNodeOptions.cs	\
	MCloud.Linode/LinodeResponse.cs 	\
	MCloud.Linode/LinodeRequest.cs		\
	MCloud.Linode/PaymentTerm.cs

LIBS=-r:System.Web.dll			\
     -r:Newtonsoft.Json.Net20.dll	\
     -r:DiffieHellman.dll  		\
     -r:Org.Mentalis.Security.dll  	\
     -r:Tamir.SharpSSH.dll		\
     -r:AWSSDK.dll

MCloud.dll: $(SOURCES)
	gmcs -target:library -out:MCloud.dll -debug $(LIBS) $(SOURCES)

test.exe: test.cs MCloud.dll
	gmcs -r:MCloud.dll -debug test.cs

ndc-setup.exe: ndc-setup.cs MCloud.dll
	gmcs -r:MCloud.dll -debug ndc-setup.cs

clean:
	rm -rf MCloud.dll
	rm -rf test.exe
