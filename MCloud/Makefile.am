
EXTRA_DIST =  

# Warning: This is an automatically generated file, do not edit!

if ENABLE_DEBUG_X86
ASSEMBLY_COMPILER_COMMAND = gmcs
ASSEMBLY_COMPILER_FLAGS =  -noconfig -codepage:utf8 -warn:4 -optimize- -debug "-define:DEBUG"
ASSEMBLY = bin/Debug/MCloud.dll
ASSEMBLY_MDB = $(ASSEMBLY).mdb
COMPILE_TARGET = library
PROJECT_REFERENCES = 
BUILD_DIR = bin/Debug

AWSSDK_DLL_SOURCE=../lib/AWSSDK.dll
DIFFIEHELLMAN_DLL_SOURCE=../lib/DiffieHellman.dll
NEWTONSOFT_JSON_NET20_DLL_SOURCE=../lib/Newtonsoft.Json.Net20.dll
ORG_MENTALIS_SECURITY_DLL_SOURCE=../lib/Org.Mentalis.Security.dll
TAMIR_SHARPSSH_DLL_SOURCE=../lib/Tamir.SharpSSH.dll
MCLOUD_DLL_MDB_SOURCE=bin/Debug/MCloud.dll.mdb
MCLOUD_DLL_MDB=$(BUILD_DIR)/MCloud.dll.mdb

endif

if ENABLE_RELEASE_X86
ASSEMBLY_COMPILER_COMMAND = gmcs
ASSEMBLY_COMPILER_FLAGS =  -noconfig -codepage:utf8 -warn:4 -optimize-
ASSEMBLY = bin/Release/MCloud.dll
ASSEMBLY_MDB = 
COMPILE_TARGET = library
PROJECT_REFERENCES = 
BUILD_DIR = bin/Release

AWSSDK_DLL_SOURCE=../lib/AWSSDK.dll
DIFFIEHELLMAN_DLL_SOURCE=../lib/DiffieHellman.dll
NEWTONSOFT_JSON_NET20_DLL_SOURCE=../lib/Newtonsoft.Json.Net20.dll
ORG_MENTALIS_SECURITY_DLL_SOURCE=../lib/Org.Mentalis.Security.dll
TAMIR_SHARPSSH_DLL_SOURCE=../lib/Tamir.SharpSSH.dll
MCLOUD_DLL_MDB=

endif

AL=al2
SATELLITE_ASSEMBLY_NAME=$(notdir $(basename $(ASSEMBLY))).resources.dll

PROGRAMFILES = \
	$(AWSSDK_DLL) \
	$(DIFFIEHELLMAN_DLL) \
	$(NEWTONSOFT_JSON_NET20_DLL) \
	$(ORG_MENTALIS_SECURITY_DLL) \
	$(TAMIR_SHARPSSH_DLL) \
	$(MCLOUD_DLL_MDB)  

LINUX_PKGCONFIG = \
	$(MCLOUD_PC)  


RESGEN=resgen2
	
all: $(ASSEMBLY) $(PROGRAMFILES) $(LINUX_PKGCONFIG) 

FILES = \
	Entity.cs \
	Node.cs \
	NodeAuth.cs \
	NodeAuthType.cs \
	NodeDriver.cs \
	NodeImage.cs \
	NodeLocation.cs \
	NodeOptions.cs \
	NodeProvider.cs \
	NodeSize.cs \
	MCloud.Deploy/Deployment.cs \
	MCloud.Deploy/MultiStepDeployment.cs \
	MCloud.Deploy/SSHDeployment.cs \
	MCloud.EC2/EC2Driver.cs \
	MCloud.EC2/EC2Node.cs \
	MCloud.EC2/EC2NodeOptions.cs \
	MCloud.EC2/EC2NodeSizes.cs \
	MCloud.Linode/LinodeAPI.cs \
	MCloud.Linode/LinodeCertificatePolicy.cs \
	MCloud.Linode/LinodeDriver.cs \
	MCloud.Linode/LinodeError.cs \
	MCloud.Linode/LinodeNode.cs \
	MCloud.Linode/LinodeNodeOptions.cs \
	MCloud.Linode/LinodeRequest.cs \
	MCloud.Linode/LinodeResponse.cs \
	MCloud.Linode/PaymentTerm.cs \
	NodeState.cs \
	MCloud.Server/ServerNode.cs \
	MCloud.Server/ServerDriver.cs \
	MCloud.Deploy/PutSSHKeys.cs \
	MCloud.Deploy/PutDirectory.cs \
	MCloud.Deploy/PutFile.cs \
	MCloud.Deploy/PutFiles.cs \
	MCloud.Deploy/RunCommand.cs \
	MCloud.Deploy/RunScript.cs \
	MCloud.Deploy/CreateUser.cs \
	MCloud.Deploy/PushNpgsqlDB.cs 

DATA_FILES = 

RESOURCES = 

EXTRAS = \
	MCloud.Server \
	mcloud.pc.in 

REFERENCES =  \
	System.Web \
	System.Core \
	System \
	Npgsql \
	ICSharpCode.SharpZipLib

DLL_REFERENCES =  \
	../lib/AWSSDK.dll \
	../lib/DiffieHellman.dll \
	../lib/Newtonsoft.Json.Net20.dll \
	../lib/Org.Mentalis.Security.dll \
	../lib/Tamir.SharpSSH.dll

CLEANFILES = $(PROGRAMFILES) $(LINUX_PKGCONFIG) 

include $(top_srcdir)/Makefile.include

AWSSDK_DLL = $(BUILD_DIR)/AWSSDK.dll
DIFFIEHELLMAN_DLL = $(BUILD_DIR)/DiffieHellman.dll
NEWTONSOFT_JSON_NET20_DLL = $(BUILD_DIR)/Newtonsoft.Json.Net20.dll
ORG_MENTALIS_SECURITY_DLL = $(BUILD_DIR)/Org.Mentalis.Security.dll
TAMIR_SHARPSSH_DLL = $(BUILD_DIR)/Tamir.SharpSSH.dll
MCLOUD_PC = $(BUILD_DIR)/mcloud.pc

$(eval $(call emit-deploy-target,AWSSDK_DLL))
$(eval $(call emit-deploy-target,DIFFIEHELLMAN_DLL))
$(eval $(call emit-deploy-target,NEWTONSOFT_JSON_NET20_DLL))
$(eval $(call emit-deploy-target,ORG_MENTALIS_SECURITY_DLL))
$(eval $(call emit-deploy-target,TAMIR_SHARPSSH_DLL))
$(eval $(call emit-deploy-wrapper,MCLOUD_PC,mcloud.pc))


$(eval $(call emit_resgen_targets))
$(build_xamlg_list): %.xaml.g.cs: %.xaml
	xamlg '$<'

$(ASSEMBLY_MDB): $(ASSEMBLY)

$(ASSEMBLY): $(build_sources) $(build_resources) $(build_datafiles) $(DLL_REFERENCES) $(PROJECT_REFERENCES) $(build_xamlg_list) $(build_satellite_assembly_list)
	mkdir -p $(shell dirname $(ASSEMBLY))
	$(ASSEMBLY_COMPILER_COMMAND) $(ASSEMBLY_COMPILER_FLAGS) -out:$(ASSEMBLY) -target:$(COMPILE_TARGET) $(build_sources_embed) $(build_resources_embed) $(build_references_ref)
