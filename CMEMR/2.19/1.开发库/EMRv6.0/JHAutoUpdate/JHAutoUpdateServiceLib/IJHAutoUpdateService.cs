using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ServiceModel;
using JHEMR.JHCommonLib.Entity.TableObject;
using System.Data;

namespace JHEMR.JHAutoUpdateServiceLib
{
    [ServiceContract(SessionMode = SessionMode.Required)]
    public interface IJHAutoUpdateService
    {
        
        #region MyRegion
        //[OperationContract(IsOneWay = true)]
        //void Test();
        //[OperationContract]
        //void GetPatientInfo(String patient_id, String visit_id, String hospital_no
        //   , out PAT_MASTER_INDEX patient_index, out PAT_VISIT patient_visit);
        //[OperationContract]
        //DataSet GetFileIndexs(String hospital_no, String patient_id, String visit_id, String file_visit_type_id);
        //[OperationContract]
        //void GetFileContentHtm(String file_unique_id, out JHMR_FILE_CONTENT_HTM content_htm);
        //[OperationContract]
        //void GetFileContentDG(String patient_id, String visit_id, String hospital_no
        //   , String dg_code, String file_visit_type_id, out JHMR_FILE_CONTENT_DG content_dg);
        #endregion

        [OperationContract]
        System.Data.DataSet get_All_UploadFile(string strDown_NO_From, out int imax_No);


        [OperationContract]
        System.Data.DataSet getList(string strDown_NO_From, out int imax_No);


        //[OperationContract]
        //System.IO.Stream getDll(string strDown_NO_From, out int imax_No);
        //[OperationContract]
        //System.IO.Compression.GZipStream get_All_UploadFile(string strDown_NO_From, out int imax_No);

        [OperationContract]
        string getStr(string str);
    }
}
