
SOURCES=Entity.cs  			\
	Deployment.cs			\
	MultiStepDeployment.cs		\
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
	ScriptDeployment.cs		\
	SSHDeployment.cs		\
	SSHKeyDeployment.cs		\
	EC2/EC2Driver.cs		\
	EC2/EC2Node.cs			\
	EC2/EC2NodeOptions.cs		\
	EC2/EC2NodeSizes.cs		\
	linode/LinodeAPI.cs		\
	linode/LinodeCertificatePolicy.cs	\
	linode/LinodeDriver.cs		\
	linode/LinodeError.cs		\
	linode/LinodeNode.cs		\
	linode/LinodeNodeOptions.cs	\
	linode/LinodeResponse.cs 	\
	linode/LinodeRequest.cs		\
	linode/PaymentTerm.cs

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

clean:
	rm -rf MCloud.dll
	rm -rf test.exe