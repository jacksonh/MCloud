
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
     -r:Newtonsoft.Json.Net20.dll	

MCloud.dll: $(SOURCES)
	gmcs -target:library -out:MCloud.dll -debug $(LIBS) $(SOURCES)

test.exe: MCloud.dll
	gmcs -r:MCloud.dll -debug test.cs

clean:
	rm -rf MCloud.dll
	rm -rf test.exe