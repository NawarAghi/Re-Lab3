<?xml version="1.0" encoding="utf-8"?>
<serviceModel xmlns:xsi="http://www.w3.org/2001/XMLSchema-instance" xmlns:xsd="http://www.w3.org/2001/XMLSchema" name="lab_3_backend" generation="1" functional="0" release="0" Id="cc942603-c076-4cea-833f-11e735fa0eaf" dslVersion="1.2.0.0" xmlns="http://schemas.microsoft.com/dsltools/RDSM">
  <groups>
    <group name="lab_3_backendGroup" generation="1" functional="0" release="0">
      <componentports>
        <inPort name="FRS_WorkerRole:Endpoint1" protocol="http">
          <inToChannel>
            <lBChannelMoniker name="/lab_3_backend/lab_3_backendGroup/LB:FRS_WorkerRole:Endpoint1" />
          </inToChannel>
        </inPort>
      </componentports>
      <settings>
        <aCS name="FRS_WorkerRole:Microsoft.WindowsAzure.Plugins.Diagnostics.ConnectionString" defaultValue="">
          <maps>
            <mapMoniker name="/lab_3_backend/lab_3_backendGroup/MapFRS_WorkerRole:Microsoft.WindowsAzure.Plugins.Diagnostics.ConnectionString" />
          </maps>
        </aCS>
        <aCS name="FRS_WorkerRoleInstances" defaultValue="[1,1,1]">
          <maps>
            <mapMoniker name="/lab_3_backend/lab_3_backendGroup/MapFRS_WorkerRoleInstances" />
          </maps>
        </aCS>
      </settings>
      <channels>
        <lBChannel name="LB:FRS_WorkerRole:Endpoint1">
          <toPorts>
            <inPortMoniker name="/lab_3_backend/lab_3_backendGroup/FRS_WorkerRole/Endpoint1" />
          </toPorts>
        </lBChannel>
      </channels>
      <maps>
        <map name="MapFRS_WorkerRole:Microsoft.WindowsAzure.Plugins.Diagnostics.ConnectionString" kind="Identity">
          <setting>
            <aCSMoniker name="/lab_3_backend/lab_3_backendGroup/FRS_WorkerRole/Microsoft.WindowsAzure.Plugins.Diagnostics.ConnectionString" />
          </setting>
        </map>
        <map name="MapFRS_WorkerRoleInstances" kind="Identity">
          <setting>
            <sCSPolicyIDMoniker name="/lab_3_backend/lab_3_backendGroup/FRS_WorkerRoleInstances" />
          </setting>
        </map>
      </maps>
      <components>
        <groupHascomponents>
          <role name="FRS_WorkerRole" generation="1" functional="0" release="0" software="D:\Year3\CLD\RE_Lab3\lab3-master\Ahmad-Abdulal-1\lab-3-backend\csx\Debug\roles\FRS_WorkerRole" entryPoint="base\x64\WaHostBootstrapper.exe" parameters="base\x64\WaWorkerHost.exe " memIndex="-1" hostingEnvironment="consoleroleadmin" hostingEnvironmentVersion="2">
            <componentports>
              <inPort name="Endpoint1" protocol="http" portRanges="80" />
            </componentports>
            <settings>
              <aCS name="Microsoft.WindowsAzure.Plugins.Diagnostics.ConnectionString" defaultValue="" />
              <aCS name="__ModelData" defaultValue="&lt;m role=&quot;FRS_WorkerRole&quot; xmlns=&quot;urn:azure:m:v1&quot;&gt;&lt;r name=&quot;FRS_WorkerRole&quot;&gt;&lt;e name=&quot;Endpoint1&quot; /&gt;&lt;/r&gt;&lt;/m&gt;" />
            </settings>
            <resourcereferences>
              <resourceReference name="DiagnosticStore" defaultAmount="[4096,4096,4096]" defaultSticky="true" kind="Directory" />
              <resourceReference name="EventStore" defaultAmount="[1000,1000,1000]" defaultSticky="false" kind="LogStore" />
            </resourcereferences>
          </role>
          <sCSPolicy>
            <sCSPolicyIDMoniker name="/lab_3_backend/lab_3_backendGroup/FRS_WorkerRoleInstances" />
            <sCSPolicyUpdateDomainMoniker name="/lab_3_backend/lab_3_backendGroup/FRS_WorkerRoleUpgradeDomains" />
            <sCSPolicyFaultDomainMoniker name="/lab_3_backend/lab_3_backendGroup/FRS_WorkerRoleFaultDomains" />
          </sCSPolicy>
        </groupHascomponents>
      </components>
      <sCSPolicy>
        <sCSPolicyUpdateDomain name="FRS_WorkerRoleUpgradeDomains" defaultPolicy="[5,5,5]" />
        <sCSPolicyFaultDomain name="FRS_WorkerRoleFaultDomains" defaultPolicy="[2,2,2]" />
        <sCSPolicyID name="FRS_WorkerRoleInstances" defaultPolicy="[1,1,1]" />
      </sCSPolicy>
    </group>
  </groups>
  <implements>
    <implementation Id="88782792-9eae-4fa4-b563-b1b1b3649549" ref="Microsoft.RedDog.Contract\ServiceContract\lab_3_backendContract@ServiceDefinition">
      <interfacereferences>
        <interfaceReference Id="2836382e-3170-4c77-bdc4-16012aa46fa4" ref="Microsoft.RedDog.Contract\Interface\FRS_WorkerRole:Endpoint1@ServiceDefinition">
          <inPort>
            <inPortMoniker name="/lab_3_backend/lab_3_backendGroup/FRS_WorkerRole:Endpoint1" />
          </inPort>
        </interfaceReference>
      </interfacereferences>
    </implementation>
  </implements>
</serviceModel>