﻿//------------------------------------------------------------------------------
// <auto-generated>
//     此代码由工具生成。
//     运行时版本:4.0.30319.296
//
//     对此文件的更改可能会导致不正确的行为，并且如果
//     重新生成代码，这些更改将会丢失。
// </auto-generated>
//------------------------------------------------------------------------------

namespace JHEMR.JHWidgetConfig.autoupdate {
    using System.Runtime.Serialization;
    using System;
    
    
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Runtime.Serialization", "4.0.0.0")]
    [System.Runtime.Serialization.DataContractAttribute(Name="FileInfo", Namespace="http://schemas.datacontract.org/2004/07/JHEMR.JHAutoUpdateServiceLib")]
    [System.SerializableAttribute()]
    public partial class FileInfo : object, System.Runtime.Serialization.IExtensibleDataObject, System.ComponentModel.INotifyPropertyChanged {
        
        [System.NonSerializedAttribute()]
        private System.Runtime.Serialization.ExtensionDataObject extensionDataField;
        
        [System.Runtime.Serialization.OptionalFieldAttribute()]
        private string ClsIDField;
        
        [System.Runtime.Serialization.OptionalFieldAttribute()]
        private byte[] FileBodyField;
        
        [System.Runtime.Serialization.OptionalFieldAttribute()]
        private string FileNameField;
        
        [System.Runtime.Serialization.OptionalFieldAttribute()]
        private string LastUpdateField;
        
        [System.Runtime.Serialization.OptionalFieldAttribute()]
        private string PathField;
        
        [global::System.ComponentModel.BrowsableAttribute(false)]
        public System.Runtime.Serialization.ExtensionDataObject ExtensionData {
            get {
                return this.extensionDataField;
            }
            set {
                this.extensionDataField = value;
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute()]
        public string ClsID {
            get {
                return this.ClsIDField;
            }
            set {
                if ((object.ReferenceEquals(this.ClsIDField, value) != true)) {
                    this.ClsIDField = value;
                    this.RaisePropertyChanged("ClsID");
                }
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute()]
        public byte[] FileBody {
            get {
                return this.FileBodyField;
            }
            set {
                if ((object.ReferenceEquals(this.FileBodyField, value) != true)) {
                    this.FileBodyField = value;
                    this.RaisePropertyChanged("FileBody");
                }
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute()]
        public string FileName {
            get {
                return this.FileNameField;
            }
            set {
                if ((object.ReferenceEquals(this.FileNameField, value) != true)) {
                    this.FileNameField = value;
                    this.RaisePropertyChanged("FileName");
                }
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute()]
        public string LastUpdate {
            get {
                return this.LastUpdateField;
            }
            set {
                if ((object.ReferenceEquals(this.LastUpdateField, value) != true)) {
                    this.LastUpdateField = value;
                    this.RaisePropertyChanged("LastUpdate");
                }
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute()]
        public string Path {
            get {
                return this.PathField;
            }
            set {
                if ((object.ReferenceEquals(this.PathField, value) != true)) {
                    this.PathField = value;
                    this.RaisePropertyChanged("Path");
                }
            }
        }
        
        public event System.ComponentModel.PropertyChangedEventHandler PropertyChanged;
        
        protected void RaisePropertyChanged(string propertyName) {
            System.ComponentModel.PropertyChangedEventHandler propertyChanged = this.PropertyChanged;
            if ((propertyChanged != null)) {
                propertyChanged(this, new System.ComponentModel.PropertyChangedEventArgs(propertyName));
            }
        }
    }
    
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    [System.ServiceModel.ServiceContractAttribute(ConfigurationName="autoupdate.IJHAutoUpdateService", SessionMode=System.ServiceModel.SessionMode.Required)]
    public interface IJHAutoUpdateService {
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IJHAutoUpdateService/UploadDLL", ReplyAction="http://tempuri.org/IJHAutoUpdateService/UploadDLLResponse")]
        bool UploadDLL(System.Collections.Generic.List<JHEMR.JHCommonLib.Entity.TableObject.JHSYS_FILE_UPLOAD> files);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IJHAutoUpdateService/StopCurrVersion", ReplyAction="http://tempuri.org/IJHAutoUpdateService/StopCurrVersionResponse")]
        bool StopCurrVersion(JHEMR.JHCommonLib.Entity.TableObject.JHSYS_FILE_UPLOAD file, string sql);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IJHAutoUpdateService/StartVision", ReplyAction="http://tempuri.org/IJHAutoUpdateService/StartVisionResponse")]
        bool StartVision(JHEMR.JHCommonLib.Entity.TableObject.JHSYS_FILE_UPLOAD file, string sql, string old_distribute_no);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IJHAutoUpdateService/GetAllCurrentFiles", ReplyAction="http://tempuri.org/IJHAutoUpdateService/GetAllCurrentFilesResponse")]
        [return: System.ServiceModel.MessageParameterAttribute(Name="max_serial_number")]
        int GetAllCurrentFiles(out System.Collections.Generic.List<JHEMR.JHWidgetConfig.autoupdate.FileInfo> file_infos, string sys_code, int client_serial_number);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IJHAutoUpdateService/GetAllSysDicts", ReplyAction="http://tempuri.org/IJHAutoUpdateService/GetAllSysDictsResponse")]
        [return: System.ServiceModel.MessageParameterAttribute(Name="dicts")]
        System.Collections.Generic.List<JHEMR.JHCommonLib.Entity.TableObject.JHSYS_DICT> GetAllSysDicts();
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IJHAutoUpdateService/GetAllFiles", ReplyAction="http://tempuri.org/IJHAutoUpdateService/GetAllFilesResponse")]
        [return: System.ServiceModel.MessageParameterAttribute(Name="files")]
        System.Collections.Generic.List<JHEMR.JHCommonLib.Entity.TableObject.JHSYS_FILE_UPLOAD> GetAllFiles(string sys_code);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IJHAutoUpdateService/GetFileInfo", ReplyAction="http://tempuri.org/IJHAutoUpdateService/GetFileInfoResponse")]
        [return: System.ServiceModel.MessageParameterAttribute(Name="file")]
        JHEMR.JHCommonLib.Entity.TableObject.JHSYS_FILE_UPLOAD GetFileInfo(string sys_code, string file_name, string distribute_no);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IJHAutoUpdateService/GetMaxSerialNo", ReplyAction="http://tempuri.org/IJHAutoUpdateService/GetMaxSerialNoResponse")]
        int GetMaxSerialNo(string sys_code);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IJHAutoUpdateService/GetSingleFile", ReplyAction="http://tempuri.org/IJHAutoUpdateService/GetSingleFileResponse")]
        JHEMR.JHCommonLib.Entity.TableObject.JHSYS_FILE_UPLOAD GetSingleFile(JHEMR.JHCommonLib.Entity.TableObject.JHSYS_FILE_UPLOAD file_info);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IJHAutoUpdateService/IsFileNameExists", ReplyAction="http://tempuri.org/IJHAutoUpdateService/IsFileNameExistsResponse")]
        bool IsFileNameExists(string sys_code, string file_name);
    }
    
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    public interface IJHAutoUpdateServiceChannel : JHEMR.JHWidgetConfig.autoupdate.IJHAutoUpdateService, System.ServiceModel.IClientChannel {
    }
    
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    public partial class JHAutoUpdateServiceClient : System.ServiceModel.ClientBase<JHEMR.JHWidgetConfig.autoupdate.IJHAutoUpdateService>, JHEMR.JHWidgetConfig.autoupdate.IJHAutoUpdateService {
        
        public JHAutoUpdateServiceClient() {
        }
        
        public JHAutoUpdateServiceClient(string endpointConfigurationName) : 
                base(endpointConfigurationName) {
        }
        
        public JHAutoUpdateServiceClient(string endpointConfigurationName, string remoteAddress) : 
                base(endpointConfigurationName, remoteAddress) {
        }
        
        public JHAutoUpdateServiceClient(string endpointConfigurationName, System.ServiceModel.EndpointAddress remoteAddress) : 
                base(endpointConfigurationName, remoteAddress) {
        }
        
        public JHAutoUpdateServiceClient(System.ServiceModel.Channels.Binding binding, System.ServiceModel.EndpointAddress remoteAddress) : 
                base(binding, remoteAddress) {
        }
        
        public bool UploadDLL(System.Collections.Generic.List<JHEMR.JHCommonLib.Entity.TableObject.JHSYS_FILE_UPLOAD> files) {
            return base.Channel.UploadDLL(files);
        }
        
        public bool StopCurrVersion(JHEMR.JHCommonLib.Entity.TableObject.JHSYS_FILE_UPLOAD file, string sql) {
            return base.Channel.StopCurrVersion(file, sql);
        }
        
        public bool StartVision(JHEMR.JHCommonLib.Entity.TableObject.JHSYS_FILE_UPLOAD file, string sql, string old_distribute_no) {
            return base.Channel.StartVision(file, sql, old_distribute_no);
        }
        
        public int GetAllCurrentFiles(out System.Collections.Generic.List<JHEMR.JHWidgetConfig.autoupdate.FileInfo> file_infos, string sys_code, int client_serial_number) {
            return base.Channel.GetAllCurrentFiles(out file_infos, sys_code, client_serial_number);
        }
        
        public System.Collections.Generic.List<JHEMR.JHCommonLib.Entity.TableObject.JHSYS_DICT> GetAllSysDicts() {
            return base.Channel.GetAllSysDicts();
        }
        
        public System.Collections.Generic.List<JHEMR.JHCommonLib.Entity.TableObject.JHSYS_FILE_UPLOAD> GetAllFiles(string sys_code) {
            return base.Channel.GetAllFiles(sys_code);
        }
        
        public JHEMR.JHCommonLib.Entity.TableObject.JHSYS_FILE_UPLOAD GetFileInfo(string sys_code, string file_name, string distribute_no) {
            return base.Channel.GetFileInfo(sys_code, file_name, distribute_no);
        }
        
        public int GetMaxSerialNo(string sys_code) {
            return base.Channel.GetMaxSerialNo(sys_code);
        }
        
        public JHEMR.JHCommonLib.Entity.TableObject.JHSYS_FILE_UPLOAD GetSingleFile(JHEMR.JHCommonLib.Entity.TableObject.JHSYS_FILE_UPLOAD file_info) {
            return base.Channel.GetSingleFile(file_info);
        }
        
        public bool IsFileNameExists(string sys_code, string file_name) {
            return base.Channel.IsFileNameExists(sys_code, file_name);
        }
    }
}
