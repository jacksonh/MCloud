
SOURCES=Entity.cs  			\
	Node.cs  			\
	NodeDriver.cs  			\
	NodeImage.cs  			\
	NodeLocation.cs 		\
	NodeProvider.cs			\
	NodeSize.cs  			\
	NodeState.cs			\
	linode/LinodeAPI.cs		\
	linode/LinodeCertificatePolicy.cs	\
	linode/LinodeDriver.cs		\
	linode/LinodeError.cs		\
	linode/LinodeJobResponse.cs	\
	linode/LinodeNode.cs		\
	linode/LinodeResponse.cs 	\
	linode/LinodeRequest.cs

LIBS=-r:System.Web.dll			\
     -r:System.Web.Extensions.dll 

MCloud.dll: $(SOURCES)
	gmcs -target:library -out:MCloud.dll $(LIBS) $(SOURCES)

test.exe: MCloud.dll
	gmcs -r:MCloud.dll test.cs