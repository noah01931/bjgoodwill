================================================================================================
=
=      北京嘉和美康信息技术有限公司
=
=             各个事业部研发部门
=

=         产品代码及文档记录备份
=
=     备份执行时间：2022年7月18日-2022年8月8日 
=     备份方式：rar 
=

=     采用整盘bitlocker加密，请务必在Windows操作系统下打开
=     ----------------------------------------------------------------------------- 
=
================================================================================================


 CMBDT       //SAP、OA、HR 系统的数据备份 （取自\\JHDFS\InAppSys\DataBaseBackup）
  |  |_HR 
  |  |_OA 
  |  |_SAP
  |
  |  
 CMCEMR       //云电子病历事业部数据备份（李世伟提供） 
  |  |_GIT
  |  |_DOCS
  |
  | 
 CMCRT       //科研事业部数据备份 
  |  |_192.168.2.28       //192.168.2.28服务器中数据备份
  |       |_GIT  
  |       |_SVN        
  |          |_1.开发库_fullbackup
  |          |_4.实施管理_fullbackup 
  |          |_Auth                   //svn权限备份
  |
  |
  CMECG        //心电事业部数据备份
  |  |__192.168.2.16                      //北京服务器数据备份
  |  |   |__SVN                        //192.168.2.16 服务器中svn数据备份，包括代码和文档
  |  |        |_1.开发库_fullbackup
  |  |        |_2.基线库_fullbackup
  |  |        |_3.产品库_fullbackup
  |  |        |_V3.5_fullbackup
  |  |        |_Auth                //svn权限备份
  |  |__西安                       // 陈秀兵提供 
  |      |__Redmine
  |      |__SVN
  |         
  | 
  CMEMR        //电子病历事业部研发数据备份    
  |  |_192.168.2.19         //192.168.2.19服务器中数据备份（包括电子病历组、临床路径组、病案归档组）
  |  |     |__SVN          //svn数据备份
  |  |     |    |_1.开发库_fullbackup  
  |  |     |    |_3.产品库_fullbackup 
  |  |     |    |_Auth     //svn权限备份
  |  |     |__TFS          // 病案归档组TFS代码备份
  |  | 
  |  | 
  |  |_192.168.2.40         //192.168.2.40服务器中数据备份（急救急诊产品部）
  |  |  |__SVN                     //svn 文档和代码备份  
  |  |      |_JH_EMIS_V2_fullbackup   
  |  |      |_JH_NICU_V2.5_fullbackup   
  |  |      |_Stephan_Academy_WEBV1.0_fullbackup  
  |  |      |_Auth              //svn权限备份
  |  |
  |  |
  |  |_192.168.2.110      //192.168.2.110 服务器中移动医务组SVN数据备份
  |  |      |__SVN        //svn数据备份
  |  |         |__01开发库_fullbackup
  |  |         |__02产品库_fullbackup
  |  |         |__03开发库_fullbackup
  |  |         |__04项目资料_fullbackup
  |  |         |__Code_fullbackup
  |  |         |__NIS_fullbackup
  |  |         |__NISManage_fullbackup
  |  |         |__NISWeb_fullbackup
  |  |         |__Auth     //svn权限备份
  |  |
  |  |
  |  |_192.168.2.131      //192.168.2.131服务器中数据备份，主要为技术支持、电子病历组、肿瘤专科组、测试组数据备份
  |  |      |__SVN        //svn数据备份
  |  |         |__1.开发库_fullbackup
  |  |         |__管理文档_fullbackup
  |  |         |__Auth     //svn权限备份
  |  |
  |  |
  |  |_192.168.2.146            // 192.168.2.146服务器中手麻重症组数据备份
  |  |     |_TFS                    //TFS服务器代码备份
  |  |     |_SVN                  //svn开发库备份
  |  |        |_1.开发库_fullbackup   
  |  |        |_2.基线库_fullbackup   
  |  |        |_3.产品库_fullbackup  
  |  |        |_4.项目实施库_fullbackup
  |  |        |_5.项目实施库_fullbackup    
  |  |        |_5.重症项目发布库_fullbackup
  |  |        |_6.开发库_fullbackup    
  |  |        |_麻醉重症产品设计库_fullbackup
  |  |        |_嘉和重症医联体_fullbackup
  |  |        |_Auth                  //svn权限备份
  |  |
  |  |
  |  |_192.168.2.18               //192.168.2.18 服务器中妇幼产品组&随访产品组数据备份
  |  |    |__Redmine              //Redmine备份
  |  |    |__Sonar                   //Sonar备份
  |  |    |__SVN                     //svn 文档和代码备份  
  |  |       |_1.开发库_fullbackup   
  |  |       |_2.基线库_fullbackup   
  |  |       |_3.产品库_fullbackup    
  |  |       |_HDUAP_fullbackup       
  |  |       |_HPLUS_fullbackup   
  |  |       |_HPOS_fullbackup 
  |  |       |_O-EMR_fullbackup 
  |  |       |_Project_fullbackup
  |  |       |_PSDC_CMMI_fullbackup
  |  |       |_SaleManagement_fullbackup   
  |  |       |_Auth                 //svn权限备份
  |  |
  |  |
  |  |_192.168.2.58         //192.168.2.58  服务器中运维服务部共享文档备份
  |  |   |__01-医院报修问题描述-普通文件
  |  |   |__02-需求分析-普通文件 
  |  |   |__04-部门各组文件夹 
  |  |   |__05-岗位变更交接文档说明-普通文件 
  |  |   |__06-产品资料 
  |  |   |__07-OEM客户报修记录跟踪平台-普通文件
  |  |   |__08-部门培训 
  |  |   |__09-常见问题处理方法 
  |  |   |__10-公共文件-普通文件                      
  |  |   |__11-年度服务报告  
  |  |   |__12-运维客户服务组  
  |  |   |__13-ITSS 文件  
  |  |   |__14-任职资格评审材料 
  |  |   |__15-质量组  
  |  |   |__16-项目交接资料 
  |  |
  |  |
  |  |_192.168.2.144             //192.168.2.144 电子病历V5代码备份
  |  |   |__  TFS                    //TFS 代码备份    
  |  |
  |  |
  |  |_192.168.16.186         //192.168.16.185 服务器中肿瘤专科组、移动医务组Git代码备份
  |  |   |__  Git                    //Git 代码备份      
  |  |    
  |  | 
  |  |_192.168.2.24         //192.168.2.24  服务器中移动医生组数据备份
  |     |__SVN                     //svn 文档和代码备份  
  |        |_1.开发库_fullbackup   
  |        |_2.基线库_fullbackup   
  |        |_3.产品库_fullbackup
  |        |_DingTalk_fullbackup   
  |        |_MobileCWR3.0_fullbackup   
  |        |_MobileRC1.0_fullbackup
  |        |_MobileWH1.0_fullbackup
  | 
  |
  CMHIP                          //  192.168.2.45服务器中HIP平台事业部数据备份
  |  |__SVN                       //svn 备份
  |  |   |_1.开发库_fullbackup  
  |  |   |_2.基线库_0_3   
  |  |   |_Auth                   
  |  |   |_hipitem_fullbackup
  |  |   |_hipsales_fullbackup 
  |  |   |_sales_fullbackup              
  |  |       
  |  |__禅道                    //禅道数据备份  （陈林相提供）
  |
  | 
  CMHDR                        //92.168.2.32  服务器中平台数据中心事业部HDR研发部数据备份    
  |  |__Redmine                //redmine平台管理数据备份 
  |  |__Git                         //Git 数据备份  
  |  |__SVN                       //svn 备份
  |     |_1.dev_fullbackup         
  |     |_2.基线库_fullbackup        
  |     |_3.产品库_fullbackup       
  |     |_Auth                    //svn权限备份
  |
  |  
  CMSYS       //192.168.2.130 服务器中股份公司系统研究部（毕兰所在组）& 平台数据中心集成平台组 数据备份
  |   |__SVN   
  |     |_1.开发库_fullbackup    //svn数据库备份，包括文档和代码
  |     |_2.基线库_fullbackup
  |     |_3.产品库_fullbackup
  |     |_4.文档库_fullbackup
  |     |_CodeReview_fullbackup
  |     |_Auth             //svn权限备份         
  |   
  |       
  CMJHIP                        //平台数据中心集成平台组 （天津 刘文艺提供）
  |     |__192.168.2.64 
  |     |       |_Redmine       //Redmine 备份
  |     | __192.168.100.6   
  |             |_DOC            //文档备份
  |  
  | 
  CMZHYY                     //平台数据中心事业部综合应用产品部    （庞少军部门，林琤提供）  
  |      |__SVN                    
  |      |__GIT     
  | 
  |
  CMSEMR                     //口腔事业部研发代码数据备份   
  |      |_TFS                    //TFS服务器代码及项目管理数据备份
  |
  |
  CMQA                         //192.168.2.36服务器中 质量部、IT运维组、财务部、法规部 数据备份  
  |  |__192.168.2.36 
  |  |  |__SVN                   
  |  |       |_1.开发库_fullbackup     
  |  |       |_2.基线库_fullbackup     
  |  |       |_3.产品库_fullbackup
  |  |       |_CH_fullbackup     
  |  |       |_CMMI_fullbackup         
  |  |       |_CMMIASS_fullbackup      
  |  |       |_CMMIBAS_fullbackup      
  |  |       |_CMMIDEV_fullbackup 
  |  |       |_Dox_fullbackup         
  |  |       |_fd_fullbackup  
  |  |       |_Goodwill_fullbackup 
  |  |       |_hipsales_fullbackup 
  |  |       |_HR_fullbackup 
  |  |       |_HSQA_fullbackup
  |  |       |_HSSQ_fullbackup  
  |  |       |_ISOBAS_fullbackup
  |  |       |_ITIL_fullbackup 
  |  |       |_SEMR_fullbackup 
  |  |       |_UEdit_fullbackup 
  |  |       |_法规部_fullbackup 
  |  |       |_管理文档_fullbackup 
  |  |       |_市场部_fullbackup   
  |  |       |_业务三部_fullbackup
  |  |       |_综合管理部_fullbackup
  |  |       |_Auth             //svn权限备份
  |  |   
  |  |__Redmine             // 质量部Redmine数据库备份，包含质量部任务管理、研发工时、国产化产品管理（192.168.103.230）   
  |  |__Products            // 产品发布库数据备份 
  |
  | 
  CMQY                   // 区域产品与数据中心数据备份  (李启瑞提供)
  |   |__SVN 
  |   |   |_project_fullbackup     
  |   |   |_rbc_fullbackup                   
  |   |   
  |   |__禅道 
  |
  |
  CMHT                                 //192.168.2.31服务器中 信息公司合同数据备份 
  |   |__SVN 
  |      |_DOC_fullbackup        //svn数据库备份
  |      |_HT_fullbackup 
  |      |_产品文档库_fullbackup
  |      |_股份财务_fullbackup
  |      |_Auth                       // 权限备份
  | 
  |   
  CMEPM                          //192.168.2.20 中公司EPM平台数据备份 
  |     |__databackup
  |     
  |
  CMUI 
  |     |__SVN                         // 192.168.2.17服务器中 UIUE组数据备份  
  |        |_UI_fullbackup
  |        |_Auth  
  |   
  | 
  |__End

  