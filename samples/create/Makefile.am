
EXTRA_DIST =  

# Warning: This is an automatically generated file, do not edit!

if ENABLE_DEBUG_X86
ASSEMBLY_COMPILER_COMMAND = gmcs
ASSEMBLY_COMPILER_FLAGS =  -noconfig -codepage:utf8 -warn:4 -optimize- -debug "-define:DEBUG"
ASSEMBLY = bin/Debug/samples.exe
ASSEMBLY_MDB = $(ASSEMBLY).mdb
COMPILE_TARGET = exe
PROJECT_REFERENCES =  \
	../MCloud/bin/Debug/MCloud.dll
BUILD_DIR = bin/Debug

LINODE_KEY_TXT_SOURCE=LINODE-KEY.txt
MCLOUD_DLL_SOURCE=../MCloud/bin/Debug/MCloud.dll
AWSSDK_DLL_SOURCE=../lib/AWSSDK.dll
DIFFIEHELLMAN_DLL_SOURCE=../lib/DiffieHellman.dll
NEWTONSOFT_JSON_NET20_DLL_SOURCE=../lib/Newtonsoft.Json.Net20.dll
ORG_MENTALIS_SECURITY_DLL_SOURCE=../lib/Org.Mentalis.Security.dll
TAMIR_SHARPSSH_DLL_SOURCE=../lib/Tamir.SharpSSH.dll
MCLOUD_DLL_MDB_SOURCE=../MCloud/bin/Debug/MCloud.dll.mdb
MCLOUD_DLL_MDB=$(BUILD_DIR)/MCloud.dll.mdb
SAMPLES_EXE_MDB_SOURCE=bin/Debug/samples.exe.mdb
SAMPLES_EXE_MDB=$(BUILD_DIR)/samples.exe.mdb

endif

if ENABLE_RELEASE_X86
ASSEMBLY_COMPILER_COMMAND = gmcs
ASSEMBLY_COMPILER_FLAGS =  -noconfig -codepage:utf8 -warn:4 -optimize-
ASSEMBLY = bin/Release/samples.exe
ASSEMBLY_MDB = 
COMPILE_TARGET = exe
PROJECT_REFERENCES =  \
	../MCloud/bin/Release/MCloud.dll
BUILD_DIR = bin/Release

LINODE_KEY_TXT_SOURCE=LINODE-KEY.txt
MCLOUD_DLL_SOURCE=../MCloud/bin/Release/MCloud.dll
AWSSDK_DLL_SOURCE=../lib/AWSSDK.dll
DIFFIEHELLMAN_DLL_SOURCE=../lib/DiffieHellman.dll
NEWTONSOFT_JSON_NET20_DLL_SOURCE=../lib/Newtonsoft.Json.Net20.dll
ORG_MENTALIS_SECURITY_DLL_SOURCE=../lib/Org.Mentalis.Security.dll
TAMIR_SHARPSSH_DLL_SOURCE=../lib/Tamir.SharpSSH.dll
MCLOUD_DLL_MDB=
SAMPLES_EXE_MDB=

endif

AL=al2
SATELLITE_ASSEMBLY_NAME=$(notdir $(basename $(ASSEMBLY))).resources.dll

PROGRAMFILES = \
	$(LINODE_KEY_TXT) \
	$(MCLOUD_DLL) \
	$(AWSSDK_DLL) \
	$(DIFFIEHELLMAN_DLL) \
	$(NEWTONSOFT_JSON_NET20_DLL) \
	$(ORG_MENTALIS_SECURITY_DLL) \
	$(TAMIR_SHARPSSH_DLL) \
	$(MCLOUD_DLL_MDB) \
	$(SAMPLES_EXE_MDB)  

BINARIES = \
	$(SAMPLES)  


RESGEN=resgen2
	
all: $(ASSEMBLY) $(PROGRAMFILES) $(BINARIES) 

FILES = \
	CreateLinode.cs \
	AssemblyInfo.cs 

DATA_FILES = 

RESOURCES = 

EXTRAS = \
	LINODE-KEY.txt \
	samples.in 

REFERENCES =  \
	System \
	System.Core \
	System.Web

DLL_REFERENCES = 

CLEANFILES = $(PROGRAMFILES) $(BINARIES) 

include $(top_srcdir)/Makefile.include

LINODE_KEY_TXT = $(BUILD_DIR)/LINODE-KEY.txt
MCLOUD_DLL = $(BUILD_DIR)/MCloud.dll
AWSSDK_DLL = $(BUILD_DIR)/AWSSDK.dll
DIFFIEHELLMAN_DLL = $(BUILD_DIR)/DiffieHellman.dll
NEWTONSOFT_JSON_NET20_DLL = $(BUILD_DIR)/Newtonsoft.Json.Net20.dll
ORG_MENTALIS_SECURITY_DLL = $(BUILD_DIR)/Org.Mentalis.Security.dll
TAMIR_SHARPSSH_DLL = $(BUILD_DIR)/Tamir.SharpSSH.dll
SAMPLES = $(BUILD_DIR)/samples

$(eval $(call emit-deploy-target,LINODE_KEY_TXT))
$(eval $(call emit-deploy-target,MCLOUD_DLL))
$(eval $(call emit-deploy-target,AWSSDK_DLL))
$(eval $(call emit-deploy-target,DIFFIEHELLMAN_DLL))
$(eval $(call emit-deploy-target,NEWTONSOFT_JSON_NET20_DLL))
$(eval $(call emit-deploy-target,ORG_MENTALIS_SECURITY_DLL))
$(eval $(call emit-deploy-target,TAMIR_SHARPSSH_DLL))
$(eval $(call emit-deploy-target,MCLOUD_DLL_MDB))
$(eval $(call emit-deploy-wrapper,SAMPLES,samples,x))


$(eval $(call emit_resgen_targets))
$(build_xamlg_list): %.xaml.g.cs: %.xaml
	xamlg '$<'

$(ASSEMBLY_MDB): $(ASSEMBLY)

$(ASSEMBLY): $(build_sources) $(build_resources) $(build_datafiles) $(DLL_REFERENCES) $(PROJECT_REFERENCES) $(build_xamlg_list) $(build_satellite_assembly_list)
	mkdir -p $(shell dirname $(ASSEMBLY))
	$(ASSEMBLY_COMPILER_COMMAND) $(ASSEMBLY_COMPILER_FLAGS) -out:$(ASSEMBLY) -target:$(COMPILE_TARGET) $(build_sources_embed) $(build_resources_embed) $(build_references_ref)
