cell_1
#region namespace imports
using System;
using System.Collections;
using System.Drawing;
using System.IO;
using System.Windows.Forms;
using Cognex.VisionPro;
using Cognex.VisionPro.ToolBlock;
using Cognex.VisionPro3D;
using Cognex.VisionPro.ImageProcessing;
#endregion

public class CogToolBlockAdvancedScript : CogToolBlockAdvancedScriptBase
{
  #region Private Member Variables
  private Cognex.VisionPro.ToolBlock.CogToolBlock mToolBlock;
  #endregion

  /// <summary>
  /// Called when the parent tool is run.
  /// Add code here to customize or replace the normal run behavior.
  /// </summary>
  /// <param name="message">Sets the Message in the tool's RunStatus.</param>
  /// <param name="result">Sets the Result in the tool's RunStatus</param>
  /// <returns>True if the tool should run normally,
  ///          False if GroupRun customizes run behavior</returns>
  public override bool GroupRun(ref string message, ref CogToolResultConstants result)
  {
    // To let the execution stop in this script when a debugger is attached, uncomment the following lines.
    // #if DEBUG
    //if (System.Diagnostics.Debugger.IsAttached) System.Diagnostics.Debugger.Break();
    // #endif


    // Run each tool using the RunTool function
    #region 输入输出
    		 
    double MIN_Up_side = (double) mToolBlock.Inputs["MIN_Up_side"].Value;
    double MAX_Up_side = (double) mToolBlock.Inputs["MAX_Up_side"].Value;
    double MIN_Low_side = (double) mToolBlock.Inputs["MIN_Low_side"].Value;
    double MAX_Low_side = (double) mToolBlock.Inputs["MAX_Low_side"].Value;
    double MIN_Left_side = (double) mToolBlock.Inputs["MIN_Left_side"].Value;
    double MAX_Left_side = (double) mToolBlock.Inputs["MAX_Left_side"].Value;
    double MIN_Right_side = (double) mToolBlock.Inputs["MIN_Right_side"].Value;
    double MAX_Right_side = (double) mToolBlock.Inputs["MAX_Right_side"].Value;
    int Long_Caliper_Count = (int) mToolBlock.Inputs["Long_Caliper_Count"].Value;
    int Short_Caliper_Count = (int) mToolBlock.Inputs["Short_Caliper_Count"].Value;
    
  //  double MAX_Two_side = (double) mToolBlock.Inputs["MAX_Two_side"].Value;
   // double MIN_Two_side = (double) mToolBlock.Inputs["MIN_Two_side"].Value;
    
    #endregion
    CogIPOneImageTool Tool_CogIPOneImageTool = new CogIPOneImageTool();
   
    CogToolBlock Tool_CogToolBlock1 = new CogToolBlock();
    CogToolBlock Tool_CogToolBlock2 = new CogToolBlock();
    CogToolBlock Tool_CogToolBlock3 = new CogToolBlock();
    CogToolBlock Tool_CogToolBlock4 = new CogToolBlock();
    
  
    
    try 
    {	        
      foreach(ICogTool tool in mToolBlock.Tools)
      {
        switch (tool.Name)
        {
          
          case "产品基准":
            Tool_CogToolBlock2 = tool as CogToolBlock;
            mToolBlock.RunTool(tool, ref message, ref result);
            if (Tool_CogToolBlock2.RunStatus.Result != CogToolResultConstants.Accept)
            {
              message += tool.Name + "工具运行出错,";
            }
          
            break;
          case "胶条找边":
            Tool_CogToolBlock3 = tool as CogToolBlock;
            Tool_CogToolBlock3.Inputs["Long_Caliper_Count"].Value = Long_Caliper_Count;
            Tool_CogToolBlock3.Inputs["Short_Caliper_Count"].Value = Short_Caliper_Count;
            Tool_CogToolBlock3.Inputs["Rectangle_Left"].Value = Tool_CogToolBlock2.Outputs["Rectangle_Left"].Value;
            Tool_CogToolBlock3.Inputs["Rectangle_Right"].Value = Tool_CogToolBlock2.Outputs["Rectangle_Right"].Value;
            Tool_CogToolBlock3.Inputs["Rectangle_Up"].Value = Tool_CogToolBlock2.Outputs["Rectangle_Up"].Value;
            Tool_CogToolBlock3.Inputs["Rectangle_Down"].Value = Tool_CogToolBlock2.Outputs["Rectangle_Down"].Value;
            Tool_CogToolBlock3.Inputs["MIN_Up_side"].Value = MIN_Up_side;
            Tool_CogToolBlock3.Inputs["MAX_Up_side"].Value = MAX_Up_side;
            Tool_CogToolBlock3.Inputs["MAX_Low_side"].Value = MAX_Low_side;
            Tool_CogToolBlock3.Inputs["MIN_Low_side"].Value = MIN_Low_side;
            Tool_CogToolBlock3.Inputs["MIN_Left_side"].Value = MIN_Left_side;
            Tool_CogToolBlock3.Inputs["MAX_Left_side"].Value = MAX_Left_side;
            Tool_CogToolBlock3.Inputs["MAX_Right_side"].Value = MAX_Right_side;
            Tool_CogToolBlock3.Inputs["MIN_Right_side"].Value = MIN_Right_side;
            mToolBlock.RunTool(tool, ref message, ref result);
          
            if (Tool_CogToolBlock3.RunStatus.Result != CogToolResultConstants.Accept)
            {
              message += tool.Name + "工具运行出错,";
            }
            else
            {
              mToolBlock.Outputs["Result"].Value = Tool_CogToolBlock3.Outputs["Result"].Value;
            }
            break;
          default:
            mToolBlock.RunTool(tool, ref message, ref result);
            break;
        }
			
      }
    }
    catch (Exception)
    {
		  mToolBlock.Outputs["Result"].Value = false;
    }
    
     
    
    
    
    
    

    return false;
  }

  #region When the Current Run Record is Created
  /// <summary>
  /// Called when the current record may have changed and is being reconstructed
  /// </summary>
  /// <param name="currentRecord">
  /// The new currentRecord is available to be initialized or customized.</param>
  public override void ModifyCurrentRunRecord(Cognex.VisionPro.ICogRecord currentRecord)
  {
  }
  #endregion

  #region When the Last Run Record is Created
  /// <summary>
  /// Called when the last run record may have changed and is being reconstructed
  /// </summary>
  /// <param name="lastRecord">
  /// The new last run record is available to be initialized or customized.</param>
  public override void ModifyLastRunRecord(Cognex.VisionPro.ICogRecord lastRecord)
  {
  }
  #endregion

  #region When the Script is Initialized
  /// <summary>
  /// Perform any initialization required by your script here
  /// </summary>
  /// <param name="host">The host tool</param>
  public override void Initialize(Cognex.VisionPro.ToolGroup.CogToolGroup host)
  {
    // DO NOT REMOVE - Call the base class implementation first - DO NOT REMOVE
    base.Initialize(host);


    // Store a local copy of the script host
    this.mToolBlock = ((Cognex.VisionPro.ToolBlock.CogToolBlock) (host));
  }
  #endregion

}



cp



#region namespace imports
using System;
using System.Collections;
using System.Drawing;
using System.IO;
using System.Windows.Forms;
using Cognex.VisionPro;
using Cognex.VisionPro.ToolBlock;
using Cognex.VisionPro3D;
using Cognex.VisionPro.ImageProcessing;
using Cognex.VisionPro.Caliper;
#endregion

public class CogToolBlockAdvancedScript : CogToolBlockAdvancedScriptBase
{
  #region Private Member Variables
  private Cognex.VisionPro.ToolBlock.CogToolBlock mToolBlock;
  #endregion

  /// <summary>
  /// Called when the parent tool is run.
  /// Add code here to customize or replace the normal run behavior.
  /// </summary>
  /// <param name="message">Sets the Message in the tool's RunStatus.</param>
  /// <param name="result">Sets the Result in the tool's RunStatus</param>
  /// <returns>True if the tool should run normally,
  ///          False if GroupRun customizes run behavior</returns>
  public override bool GroupRun(ref string message, ref CogToolResultConstants result)
  {
    // To let the execution stop in this script when a debugger is attached, uncomment the following lines.
    // #if DEBUG
    // if (System.Diagnostics.Debugger.IsAttached) System.Diagnostics.Debugger.Break();
    // #endif


    // Run each tool using the RunTool function
    #region 生成数组类型
    double[] ledf = new double[10];
    CogToolBlockTerminal newT = new CogToolBlockTerminal("Left", ledf);
    if (mToolBlock.Outputs.Contains("Left") == false)
    {
      mToolBlock.Outputs.Add(newT);
    }
    CogToolBlockTerminal newR = new CogToolBlockTerminal("Right", ledf);
    if (mToolBlock.Outputs.Contains("Right") == false)
    {
      mToolBlock.Outputs.Add(newR);
    }
    CogToolBlockTerminal newU = new CogToolBlockTerminal("Up", ledf);
    if (mToolBlock.Outputs.Contains("Up") == false)
    {
      mToolBlock.Outputs.Add(newU);
      }  CogToolBlockTerminal newD = new CogToolBlockTerminal("Down", ledf);
    if (mToolBlock.Outputs.Contains("Down") == false)
    {
      mToolBlock.Outputs.Add(newD);
    }
    #endregion
    #region 变量声明
    CogFindLineTool Tool_CogFindLineTool1 = new CogFindLineTool();
    CogFindLineTool Tool_CogFindLineTool2 = new CogFindLineTool();
    CogFindLineTool Tool_CogFindLineTool3 = new CogFindLineTool();
    CogFindLineTool Tool_CogFindLineTool4 = new CogFindLineTool();
    #endregion
    			
    #region 工具运行
    CogToolBlock toolBlock1 = mToolBlock.Tools["CogToolBlock1"] as CogToolBlock;
    toolBlock1.Run();
    foreach(ICogTool tool in mToolBlock.Tools)
      switch (tool.Name)
      {
        case "Up":
          Tool_CogFindLineTool1 = tool as CogFindLineTool;
          mToolBlock.RunTool(tool, ref message, ref result);
          if (Tool_CogFindLineTool1.Results.NumPointsFound == 0)
          {
            message = tool.Name + "工具运行错误，未找到边！";
            //return false;
          }
          double [] Up = new double[Tool_CogFindLineTool1.Results.Count];
          for (int i = 0; i < Tool_CogFindLineTool1.Results.Count; i++)
          {
            if (Tool_CogFindLineTool1.Results[i].Found)
            {
              Up[i] = Tool_CogFindLineTool1.Results[i].X;
            
            }
         
          }
          mToolBlock.Outputs["Up"].Value = Up;
          
          break;
        case "Down":
          Tool_CogFindLineTool2 = tool as CogFindLineTool;
          mToolBlock.RunTool(tool, ref message, ref result);
          if (Tool_CogFindLineTool2.Results.NumPointsFound == 0)
          {
            message += ";" + tool.Name + "工具运行错误，未找到边！";
            //return false;
          }
          double [] Down = new double[Tool_CogFindLineTool2.Results.Count];
          for (int i = 0; i < Tool_CogFindLineTool2.Results.Count; i++)
          {
            if (Tool_CogFindLineTool2.Results[i].Found)
            {
              Down[i] = Tool_CogFindLineTool2.Results[i].X;
            
            }
           
          }
          mToolBlock.Outputs["Down"].Value = Down;
          break;
        case "Left":
          Tool_CogFindLineTool3 = tool as CogFindLineTool;
          mToolBlock.RunTool(tool, ref message, ref result);
          if (Tool_CogFindLineTool3.Results.NumPointsFound == 0)
          {
            message += ";" + tool.Name + "工具运行错误，未找到边！";
            //return false;
          }
          double [] left = new double[Tool_CogFindLineTool3.Results.Count];
          for (int i = 0; i < Tool_CogFindLineTool3.Results.Count; i++)
          {
            if (Tool_CogFindLineTool3.Results[i].Found)
            {
              left[i] = Tool_CogFindLineTool3.Results[i].Y;
            
            }
            
          }
          mToolBlock.Outputs["Left"].Value = left;
          break;
        case "Right":
          Tool_CogFindLineTool4 = tool as CogFindLineTool;
          mToolBlock.RunTool(tool, ref message, ref result);
          if (Tool_CogFindLineTool4.Results.NumPointsFound == 0)
          {
            message += ";" + tool.Name + "工具运行错误，未找到边！";
            //return false;
          }
          double [] RIGHT = new double[Tool_CogFindLineTool4.Results.Count];
          for (int i = 0; i < Tool_CogFindLineTool4.Results.Count; i++)
          {
            if (Tool_CogFindLineTool4.Results[i].Found)
            {
              RIGHT[i] = Tool_CogFindLineTool4.Results[i].Y;
            
            }
          
          }
          mToolBlock.Outputs["Right"].Value = RIGHT;
          break;
        default:
          mToolBlock.RunTool(tool, ref message, ref result);
          
          break;
      }
    #endregion
    			
    
    
			
      

    return false;
  }

  #region When the Current Run Record is Created
  /// <summary>
  /// Called when the current record may have changed and is being reconstructed
  /// </summary>
  /// <param name="currentRecord">
  /// The new currentRecord is available to be initialized or customized.</param>
  public override void ModifyCurrentRunRecord(Cognex.VisionPro.ICogRecord currentRecord)
  {
  }
  #endregion

  #region When the Last Run Record is Created
  /// <summary>
  /// Called when the last run record may have changed and is being reconstructed
  /// </summary>
  /// <param name="lastRecord">
  /// The new last run record is available to be initialized or customized.</param>
  public override void ModifyLastRunRecord(Cognex.VisionPro.ICogRecord lastRecord)
  {
  }
  #endregion

  #region When the Script is Initialized
  /// <summary>
  /// Perform any initialization required by your script here
  /// </summary>
  /// <param name="host">The host tool</param>
  public override void Initialize(Cognex.VisionPro.ToolGroup.CogToolGroup host)
  {
    // DO NOT REMOVE - Call the base class implementation first - DO NOT REMOVE
    base.Initialize(host);


    // Store a local copy of the script host
    this.mToolBlock = ((Cognex.VisionPro.ToolBlock.CogToolBlock)(host));
  }
  #endregion

}



jt


#region namespace imports
using System;
using System.Collections;
using System.Drawing;
using System.IO;
using System.Windows.Forms;
using Cognex.VisionPro;
using Cognex.VisionPro.ToolBlock;
using Cognex.VisionPro3D;
using Cognex.VisionPro.ColorExtractor;
using Cognex.VisionPro.ImageProcessing;
using Cognex.VisionPro.Blob;
using Cognex.VisionPro.Caliper;
using Cognex.VisionPro.Dimensioning;
#endregion

public class CogToolBlockAdvancedScript : CogToolBlockAdvancedScriptBase
{
  #region Private Member Variables
  private Cognex.VisionPro.ToolBlock.CogToolBlock mToolBlock;
  private Cognex.VisionPro.CogGraphicCollection mCogGraphicCollection = new CogGraphicCollection();
  //private Cognex.VisionPro.CogGraphicLabel mCogGraphicLabel;
  #endregion

  /// <summary>
  /// Called when the parent tool is run.
  /// Add code here to customize or replace the normal run behavior.
  /// </summary>
  /// <param name="message">Sets the Message in the tool's RunStatus.</param>
  /// <param name="result">Sets the Result in the tool's RunStatus</param>
  /// <returns>True if the tool should run normally,
  ///          False if GroupRun customizes run behavior</returns>
  public override bool GroupRun(ref string message, ref CogToolResultConstants result)
  {
    // To let the execution stop in this script when a debugger is attached, uncomment the following lines.
    // #if DEBUG
    //  if (System.Diagnostics.Debugger.IsAttached) System.Diagnostics.Debugger.Break();
    // #endif


    // Run each tool using the RunTool function
    mCogGraphicCollection.Clear();
    mToolBlock.Outputs["find_side_Result"].Value = false;
    #region 输入
    double MIN_Up_side = (double) mToolBlock.Inputs["MIN_Up_side"].Value;
    double MAX_Up_side = (double) mToolBlock.Inputs["MAX_Up_side"].Value;
    double MIN_Low_side = (double) mToolBlock.Inputs["MIN_Low_side"].Value;
    double MAX_Low_side = (double) mToolBlock.Inputs["MAX_Low_side"].Value;
    double MIN_Left_side = (double) mToolBlock.Inputs["MIN_Left_side"].Value;
    double MAX_Left_side = (double) mToolBlock.Inputs["MAX_Left_side"].Value;
    double MIN_Right_side = (double) mToolBlock.Inputs["MIN_Right_side"].Value;
    double MAX_Right_side = (double) mToolBlock.Inputs["MAX_Right_side"].Value;
    int Long_Caliper_Count = (int) mToolBlock.Inputs["Long_Caliper_Count"].Value;
    int Short_Caliper_Count = (int) mToolBlock.Inputs["Short_Caliper_Count"].Value;
    double Offset = (double) mToolBlock.Inputs["Offset"].Value;
    
    double Angle_UP = 0;
    double [] left = new double[13];
    left = (double []) mToolBlock.Inputs["Left"].Value;
    double [] Right = new double[13];
    Right = (double []) mToolBlock.Inputs["Right"].Value;
    double [] Up = new double[13];
    Up = (double []) mToolBlock.Inputs["Up"].Value;
    double [] Down = new double[13];
    Down = (double []) mToolBlock.Inputs["Down"].Value;
    #endregion
    			
    #region 变量声明
    CogColorExtractorTool color = mToolBlock.Tools["CogColorExtractorTool1"] as CogColorExtractorTool;
    CogImageConvertTool imageMp = mToolBlock.Tools["CogImageConvertTool1"] as CogImageConvertTool;
    CogBlobTool mBlob = mToolBlock.Tools["CogBlobTool1"] as CogBlobTool;
    CogBlobTool mBlob2 = mToolBlock.Tools["CogBlobTool2"] as CogBlobTool;
    CogFindLineTool Tool_CogFindLineTool1 = mToolBlock.Tools["CogFindLineTool1_Up"] as CogFindLineTool;
    CogFindLineTool Tool_CogFindLineTool2 = mToolBlock.Tools["CogFindLineTool2_Down"] as CogFindLineTool;
    CogFindLineTool Tool_CogFindLineTool3 = mToolBlock.Tools["CogFindLineTool3_Left"] as CogFindLineTool;
    CogFindLineTool Tool_CogFindLineTool4 = mToolBlock.Tools["CogFindLineTool4_Right"] as CogFindLineTool;
    CogDistancePointLineTool Distance_Up = (CogDistancePointLineTool) mToolBlock.Tools["Distance_Up"];
    CogDistancePointLineTool Distance_Down = (CogDistancePointLineTool) mToolBlock.Tools["Distance_Down"];
    CogDistancePointLineTool Distance_Left = (CogDistancePointLineTool) mToolBlock.Tools["Distance_Left"];
    CogDistancePointLineTool Distance_Right = (CogDistancePointLineTool) mToolBlock.Tools["Distance_Right"];
    int Pattern_Angle = (int) mToolBlock.Inputs["Pattern_Angle"].Value;
    CogLine Rectangle_Left = (CogLine) mToolBlock.Inputs["Rectangle_Left"].Value;
    CogLine Rectangle_Right = (CogLine) mToolBlock.Inputs["Rectangle_Right"].Value;
    CogLine Rectangle_Up = (CogLine) mToolBlock.Inputs["Rectangle_Up"].Value;
    CogLine Rectangle_Down = (CogLine) mToolBlock.Inputs["Rectangle_Down"].Value;
    string Data_Str1="",Data_Str2 = "",Data_Str3 = "",Data_Str4 = "";
    
    CogToolBlock toolBlock1 = mToolBlock.Tools["CogToolBlock1"] as CogToolBlock;
    
    double Line1_X,Line1_Y;
    double DistancePointLine1=0,DistancePointLine2 = 0,DistancePointLine3 = 0,DistancePointLine4 = 0;
    int Total_Result = 0;
    int Total_Result1 = 0;
    
    bool toosResult = true;
    #endregion
    
    #region 工具运行
    
    color.Run();
    imageMp.Run();
    #region 判断离型纸有无
    mBlob.Run();
    if (mBlob.Results.GetBlobs().Count > 0)
    {
       mToolBlock.Outputs["find_side_Result"].Value = true;
      message = mBlob.Name + "工具运行错误，有离心纸残留！";
      CogGraphicLabel myLabel = new CogGraphicLabel();
      myLabel.SelectedSpaceName = "#";
      myLabel.Color = CogColorConstants.Red;
      myLabel.Font = new Font("宋体", 20);
      myLabel.SetXYText(3000, 500, message);
      mCogGraphicCollection.Add(myLabel);
      mToolBlock.Outputs["Result"].Value = false;
      return false;
    }
    
    mBlob2.Run();
    if (mBlob2.Results.GetBlobs().Count > 0)
    {
           
      for (int i = 0; i < mBlob2.Results.GetBlobs().Count; i++)
      {
        message = mBlob2.Name + "工具运行错误，异物！";
        CogGraphicLabel myLabel2 = new CogGraphicLabel();
        //myLabel2.SelectedSpaceName = "#";
        myLabel2.Color = CogColorConstants.Red;
        myLabel2.Font = new Font("宋体", 30);
        myLabel2.SetXYText(mBlob2.Results.GetBlobs()[i].CenterOfMassX, mBlob2.Results.GetBlobs()[i].CenterOfMassY, message);
        mCogGraphicCollection.Add(myLabel2);
        mToolBlock.Outputs["Result"].Value = false;
      }
    }  
    #endregion
    #region 上下抓边
    #region 先抓取上边
    Tool_CogFindLineTool1.Run();
    if (Tool_CogFindLineTool1.Results.NumPointsFound == 0 )
    {
      mToolBlock.Outputs["find_side_Result"].Value = true;
      message = Tool_CogFindLineTool1.Name + "工具运行错误，漏贴胶或贴胶倾斜！";
      CogGraphicLabel myLabel = new CogGraphicLabel();
      myLabel.SelectedSpaceName = "#";
      myLabel.Color = CogColorConstants.Red;
      myLabel.Font = new Font("宋体", 20);
      myLabel.SetXYText(3000, 500, message);
      mCogGraphicCollection.Add(myLabel);
      mToolBlock.Outputs["Result"].Value = false;
      //return false;
       
    }
    else
    {
      for (int i = 0; i < Tool_CogFindLineTool1.Results.NumPointsFound; i++)
      {
        if (Tool_CogFindLineTool1.Results[i].Used == true)
        {
          Distance_Up.X = Tool_CogFindLineTool1.Results[i].X;
          Distance_Up.Y = Tool_CogFindLineTool1.Results[i].Y;
          Distance_Up.Line = Rectangle_Up;
          Distance_Up.Run();
                
          //          if (Tool_CogFindLineTool1.Results[i].X > Up[i])
          //          {
          //            DistancePointLine1 = Distance_Up.Distance;
          //          }
          //          else
          //          {
          //            DistancePointLine1 = Distance_Up.Distance;
          //          }
          DistancePointLine1 = Distance_Up.Distance;
          Line1_X = Distance_Up.LineX;
          Line1_Y = Distance_Up.LineY;
          
          
          if (  Distance_Up.X >  Line1_X)
          {
		           DistancePointLine1 = -Distance_Up.Distance;
          }
                
          CogLineSegment mCogLineSegment1 = new CogLineSegment();
          CogGraphicLabel mLabel1 = new CogGraphicLabel();
          mLabel1.Rotation = CogMisc.DegToRad(-90);
          if (DistancePointLine1 > MIN_Up_side && DistancePointLine1 < MAX_Up_side)
          {
            mCogLineSegment1.SetStartEnd(Tool_CogFindLineTool1.Results[i].X, Tool_CogFindLineTool1.Results[i].Y, Line1_X, Line1_Y);
            mCogLineSegment1.Color = CogColorConstants.Blue;
            mLabel1.SetXYText(mCogLineSegment1.MidpointX, mCogLineSegment1.MidpointY, DistancePointLine1.ToString("F2"));
            mLabel1.Color = CogColorConstants.Green;
            //mLabel1.Rotation = 180;
          }
          else
          {
            mCogLineSegment1.SetStartEnd(Tool_CogFindLineTool1.Results[i].X, Tool_CogFindLineTool1.Results[i].Y, Line1_X, Line1_Y);
            mCogLineSegment1.Color = CogColorConstants.Cyan;
            mLabel1.SetXYText(mCogLineSegment1.MidpointX, mCogLineSegment1.MidpointY, DistancePointLine1.ToString("F2"));
            mLabel1.Color = CogColorConstants.Red;
                  
                  
                  
                  
            toosResult = false;     
            Total_Result++;
          }
          Data_Str1 += DistancePointLine1.ToString("F2") + ",";
          mCogGraphicCollection.Add(mCogLineSegment1);
          mCogGraphicCollection.Add(mLabel1);
        }
             
             
      } 
   
      
    }
    #endregion
    #region 上边抓边失败启用下边
    // if (toosResult == false)
    {
     // toosResult = true;
      //Total_Result = 0;
      Tool_CogFindLineTool2.Run();
      if (Tool_CogFindLineTool2.Results.NumPointsFound == 0)
      {
        mToolBlock.Outputs["find_side_Result"].Value = true;
        message += ";" + Tool_CogFindLineTool2.Name + "工具运行错误，未找到边！";
        Total_Result++;
      }
      else
      {
        for (int i = 0; i < Tool_CogFindLineTool2.Results.NumPointsFound; i++)
        {
          if (Tool_CogFindLineTool2.Results[i].Used == true)
          {
            Distance_Down.X = Tool_CogFindLineTool2.Results[i].X;
            Distance_Down.Y = Tool_CogFindLineTool2.Results[i].Y;
            Distance_Down.Line = Rectangle_Down;
            Distance_Down.Run();
                
            //          if (Tool_CogFindLineTool2.Results[i].X < Down[i])
            //          {
            //            DistancePointLine2 = -Distance_Down.Distance;
            //          }
            //          else
            //          {
            //            DistancePointLine2 = Distance_Down.Distance;
            //          }
            DistancePointLine2 = Distance_Down.Distance;
            Line1_X = Distance_Down.LineX;
            Line1_Y = Distance_Down.LineY;
            
            
            
            if ( Distance_Down.X < Line1_X)
            {
               DistancePointLine2 = -Distance_Down.Distance;
            }
            CogLineSegment mCogLineSegment2 = new CogLineSegment();
            CogGraphicLabel mLabel2 = new CogGraphicLabel();
            mLabel2.Rotation = CogMisc.DegToRad(-90);
            if (DistancePointLine2 > MIN_Low_side && DistancePointLine2 < MAX_Low_side)
            {
              mCogLineSegment2.SetStartEnd(Tool_CogFindLineTool2.Results[i].X, Tool_CogFindLineTool2.Results[i].Y, Line1_X, Line1_Y);
              mCogLineSegment2.Color = CogColorConstants.Blue;
              // mCogLineSegment2.SelectedSpaceName = @"\Checkerboard Calibration\Fixture22";
              mLabel2.SetXYText(mCogLineSegment2.MidpointX, mCogLineSegment2.MidpointY, DistancePointLine2.ToString("F2"));
              mLabel2.Color = CogColorConstants.Green;
                
            }
            else
            {
              mCogLineSegment2.SetStartEnd(Tool_CogFindLineTool2.Results[i].X, Tool_CogFindLineTool2.Results[i].Y, Line1_X, Line1_Y);
              mCogLineSegment2.Color = CogColorConstants.Cyan;
              mLabel2.SetXYText(mCogLineSegment2.MidpointX, mCogLineSegment2.MidpointY, DistancePointLine2.ToString("F2"));
              mLabel2.Color = CogColorConstants.Red;
              Total_Result++;
            }
            Data_Str2 += DistancePointLine2.ToString("F2") + ",";
            mCogGraphicCollection.Add(mCogLineSegment2);
            mCogGraphicCollection.Add(mLabel2);
          }
         
              
            
              
        }
      }
		 
    }
    #endregion
    #endregion
    #region 左右抓边
    #region 先抓取左边
    Tool_CogFindLineTool3.Run();
    if (Tool_CogFindLineTool3.Results.NumPointsFound == 0)
    {
      mToolBlock.Outputs["find_side_Result"].Value = true;
      message += ";" + Tool_CogFindLineTool3.Name + "工具运行错误，未找到边！";
      Total_Result1++;
      //return false;
    }
    else
    {
      for (int i = 0; i < Tool_CogFindLineTool3.Results.NumPointsFound; i++)
      {
        if (Tool_CogFindLineTool3.Results[i].Used == true)
        {
          Distance_Left.X = Tool_CogFindLineTool3.Results[i].X;
          Distance_Left.Y = Tool_CogFindLineTool3.Results[i].Y;
          Distance_Left.Line = Rectangle_Left;
          Distance_Left.Run();
                 
//          if (Tool_CogFindLineTool3.Results[i].Y < left[i])
//          {
//            DistancePointLine3 = Distance_Left.Distance;
//          }
//          else
//          {
//            DistancePointLine3 = -Distance_Left.Distance;
//          }
          DistancePointLine3 = Distance_Left.Distance + Offset;
          Line1_X = Distance_Left.LineX;
          Line1_Y = Distance_Left.LineY;
          
          if (Distance_Left.Y>Line1_Y)
          {
		      DistancePointLine3 = -(Distance_Left.Distance);
          }
          
          
          CogLineSegment mCogLineSegment3 = new CogLineSegment();
          CogGraphicLabel mLabel3 = new CogGraphicLabel();
          mLabel3.Rotation = CogMisc.DegToRad(-90);
          if (DistancePointLine3 > MIN_Left_side && DistancePointLine3 < MAX_Left_side)
          {
            mCogLineSegment3.SetStartEnd(Tool_CogFindLineTool3.Results[i].X, Tool_CogFindLineTool3.Results[i].Y, Line1_X, Line1_Y);
            mCogLineSegment3.Color = CogColorConstants.Blue;
            mLabel3.SetXYText(mCogLineSegment3.MidpointX, mCogLineSegment3.MidpointY, DistancePointLine3.ToString("F2"));
            mLabel3.Color = CogColorConstants.Green;
                
          }
          else
          {
            mCogLineSegment3.SetStartEnd(Tool_CogFindLineTool3.Results[i].X, Tool_CogFindLineTool3.Results[i].Y, Line1_X, Line1_Y);
            mCogLineSegment3.Color = CogColorConstants.Cyan;
            mLabel3.SetXYText(mCogLineSegment3.MidpointX, mCogLineSegment3.MidpointY, DistancePointLine3.ToString("F2"));
            mLabel3.Color = CogColorConstants.Red;
            Total_Result1++;
                  
            toosResult = false;
          }
          Data_Str3 += DistancePointLine3.ToString("F2") + ",";
          mCogGraphicCollection.Add(mCogLineSegment3);
          mCogGraphicCollection.Add(mLabel3);
        }
             
      }
      
              
    }
    #endregion
    #region 左边抓边错误启用右边
    //  if (toosResult == false)
    {
     // Total_Result1 = 0;
     // toosResult = true;
      Tool_CogFindLineTool4.Run();
      if (Tool_CogFindLineTool4.Results.NumPointsFound == 0)
      {
        mToolBlock.Outputs["find_side_Result"].Value = true;
        message += ";" + Tool_CogFindLineTool4.Name + "工具运行错误，未找到边！";
        Total_Result1++;
        //return false;
      }
      else
      {
        for (int i = 0; i < Tool_CogFindLineTool4.Results.NumPointsFound; i++)
        {
          if (Tool_CogFindLineTool4.Results[i].Used == true) {
            Distance_Right.X = Tool_CogFindLineTool4.Results[i].X;
            Distance_Right.Y = Tool_CogFindLineTool4.Results[i].Y;
            Distance_Right.Line = Rectangle_Right;
            Distance_Right.Run();

            //          if (Tool_CogFindLineTool4.Results[i].Y > Right[i])
            //          {
            //            DistancePointLine4 = Distance_Right.Distance;
            //          }
            //          else
            //          {
            //            DistancePointLine4 = -Distance_Right.Distance;
            //          }
            DistancePointLine4 = Distance_Right.Distance;
            Line1_X = Distance_Right.LineX;
            Line1_Y = Distance_Right.LineY;
          
            if (Distance_Right.Y < Line1_Y)
            {
               mToolBlock.Outputs["find_side_Result"].Value = true;
              DistancePointLine4 = -Distance_Right.Distance;
            }
            CogLineSegment mCogLineSegment4 = new CogLineSegment();
            CogGraphicLabel mLabel4 = new CogGraphicLabel();
            mLabel4.Rotation = CogMisc.DegToRad(-90);
            if (DistancePointLine4 >= MIN_Right_side && DistancePointLine4 < MAX_Right_side)//DistancePointLine4 > MIN_Right_side &&
            {
              mCogLineSegment4.SetStartEnd(Tool_CogFindLineTool4.Results[i].X, Tool_CogFindLineTool4.Results[i].Y, Line1_X, Line1_Y);
              mCogLineSegment4.Color = CogColorConstants.Blue;
              mLabel4.SetXYText(mCogLineSegment4.MidpointX, mCogLineSegment4.MidpointY, DistancePointLine4.ToString("F2"));
              mLabel4.Color = CogColorConstants.Green;

            }
            else
            {
              mCogLineSegment4.SetStartEnd(Tool_CogFindLineTool4.Results[i].X, Tool_CogFindLineTool4.Results[i].Y, Line1_X, Line1_Y);
              mCogLineSegment4.Color = CogColorConstants.Cyan;
              mLabel4.SetXYText(mCogLineSegment4.MidpointX, mCogLineSegment4.MidpointY, DistancePointLine4.ToString("F2"));
              mLabel4.Color = CogColorConstants.Red;
              Total_Result1++;
            }
            Data_Str4 += DistancePointLine4.ToString("F2") + ",";
            mCogGraphicCollection.Add(mCogLineSegment4);
            mCogGraphicCollection.Add(mLabel4);
          }
          }
          



      }
    }
    
    toolBlock1.Run();
    bool t_result = (bool)toolBlock1.Outputs["Result"].Value;
    #endregion
    #endregion
   
      
    
    #endregion

    string l;
    
    //判断基准边和胶边不能为空，否则NG
   
    
    if (Rectangle_Left != null && Rectangle_Right != null && Rectangle_Up != null && Rectangle_Down != null && Tool_CogFindLineTool1.Results.GetLine() != null && Tool_CogFindLineTool2.Results.GetLine() != null && Tool_CogFindLineTool3.Results.GetLine() != null && Tool_CogFindLineTool4.Results.GetLine() != null)
    {
     
    }
    else
    {
      Total_Result++;
    }
    if (Total_Result > 0 || Total_Result1 > 0||!t_result)
    {
      mToolBlock.Outputs["Result"].Value = false;
      l = ",NG";
    }
    else
    {
      mToolBlock.Outputs["Result"].Value = true;
      l = ",OK";
    }
    CSV1(DateTime.Now.ToString("HH:mm:ss") + "," + Data_Str1 + l, @"E:\\Data\\Cam1\\第一段\\Up_Data");
    CSV2(DateTime.Now.ToString("HH:mm:ss") + "," + Data_Str2 + l, @"E:\\Data\\Cam1\\第一段\\Down_Data");
    CSV2(DateTime.Now.ToString("HH:mm:ss") + "," + Data_Str3 + l, @"E:\\Data\\Cam1\\第一段\\Left_Data");
    CSV4(DateTime.Now.ToString("HH:mm:ss") + "," + Data_Str4 + l, @"E:\\Data\\Cam1\\第一段\\Right_Data");
    
    return false;
  }
  #region 保存数据1
  string Data_Name1;
  public void CSV1(string Data, string path)
  {
    int Long_Caliper_Count = (int) mToolBlock.Inputs["Long_Caliper_Count"].Value;
    //int Short_Caliper_Count = (int) mToolBlock.Inputs["Short_Caliper_Count"].Value;
    if (!Directory.Exists(path))
    {
      Directory.CreateDirectory(path);
    }
    string filepath = path + "\\" + DateTime.Now.ToString("yyyyMMdd") + ".csv";
    if (!File.Exists(filepath))
    {
      File.Create(filepath).Dispose();
      using (StreamWriter sw = new StreamWriter(filepath, true))
      {
        for (int i = 0; i < Long_Caliper_Count; i++)
        {
          Data_Name1 += "Distance" + (i + 1) + ",";
         
        }
        sw.WriteLine("Time," + Data_Name1 + ",Result");
        sw.Close();
      }
    }
    using (StreamWriter sw = new StreamWriter(filepath, true))
    {
      sw.WriteLine(Data);
      sw.Close();
    } 
  }
  #endregion
  #region 保存数据2
  string Data_Name2;
  public void CSV2(string Data, string path)
  {
    int Short_Caliper_Count = (int) mToolBlock.Inputs["Short_Caliper_Count"].Value;
    if (!Directory.Exists(path))
    {
      Directory.CreateDirectory(path);
    }
    string filepath = path + "\\" + DateTime.Now.ToString("yyyyMMdd") + ".csv";
    if (!File.Exists(filepath))
    {
      File.Create(filepath).Dispose();
      using (StreamWriter sw = new StreamWriter(filepath, true))
      {
        for (int i = 0; i < Short_Caliper_Count; i++)
        {
          Data_Name2 += "Distance" + (i + 1) + ",";
        
        }
        sw.WriteLine("Time," + Data_Name2 + ",Result");
        sw.Close();
      }
    }
    using (StreamWriter sw = new StreamWriter(filepath, true))
    {
      sw.WriteLine(Data);
      sw.Close();
    } 
  }
  #endregion		
  #region 保存数据3
  string Data_Name3;
  public void CSV3(string Data, string path)
  {
    int Long_Caliper_Count = (int) mToolBlock.Inputs["Long_Caliper_Count"].Value;
    //int Short_Caliper_Count = (int) mToolBlock.Inputs["Short_Caliper_Count"].Value;
    if (!Directory.Exists(path))
    {
      Directory.CreateDirectory(path);
    }
    string filepath = path + "\\" + DateTime.Now.ToString("yyyyMMdd") + ".csv";
    if (!File.Exists(filepath))
    {
      File.Create(filepath).Dispose();
      using (StreamWriter sw = new StreamWriter(filepath, true))
      {
        for (int i = 0; i < Long_Caliper_Count; i++)
        {
          Data_Name3 += "Distance" + (i + 1) + ",";
         
        }
        sw.WriteLine("Time," + Data_Name3 + ",Result");
        sw.Close();
      }
    }
    using (StreamWriter sw = new StreamWriter(filepath, true))
    {
      sw.WriteLine(Data);
      sw.Close();
    } 
  }
  #endregion
  #region 保存数据4
  string Data_Name4;
  public void CSV4(string Data, string path)
  {
    int Short_Caliper_Count = (int) mToolBlock.Inputs["Short_Caliper_Count"].Value;
    if (!Directory.Exists(path))
    {
      Directory.CreateDirectory(path);
    }
    string filepath = path + "\\" + DateTime.Now.ToString("yyyyMMdd") + ".csv";
    if (!File.Exists(filepath))
    {
      File.Create(filepath).Dispose();
      using (StreamWriter sw = new StreamWriter(filepath, true))
      {
        for (int i = 0; i < Short_Caliper_Count; i++)
        {
          Data_Name4 += "Distance" + (i + 1) + ",";
         
        }
        sw.WriteLine("Time," + Data_Name4 + ",Result");
        sw.Close();
      }
    }
    using (StreamWriter sw = new StreamWriter(filepath, true))
    {
      sw.WriteLine(Data);
      sw.Close();
    } 
  }
  #endregion		
  
           
  #region When the Current Run Record is Created
  /// <summary>
  /// Called when the current record may have changed and is being reconstructed
  /// </summary>
  /// <param name="currentRecord">
  /// The new currentRecord is available to be initialized or customized.</param>
  public override void ModifyCurrentRunRecord(Cognex.VisionPro.ICogRecord currentRecord)
  {
  }
  #endregion

  #region When the Last Run Record is Created
  /// <summary>
  /// Called when the last run record may have changed and is being reconstructed
  /// </summary>
  /// <param name="lastRecord">
  /// The new last run record is available to be initialized or customized.</param>
  public override void ModifyLastRunRecord(Cognex.VisionPro.ICogRecord lastRecord)
  {
    foreach (ICogGraphic mGraphic in mCogGraphicCollection)
    {
      mToolBlock.AddGraphicToRunRecord(mGraphic, lastRecord, "CogColorExtractorTool1.InputImage", "");
    }
  }
  #endregion

  #region When the Script is Initialized
  /// <summary>
  /// Perform any initialization required by your script here
  /// </summary>
  /// <param name="host">The host tool</param>
  public override void Initialize(Cognex.VisionPro.ToolGroup.CogToolGroup host)
  {
    // DO NOT REMOVE - Call the base class implementation first - DO NOT REMOVE
    base.Initialize(host);


    // Store a local copy of the script host
    this.mToolBlock = ((Cognex.VisionPro.ToolBlock.CogToolBlock) (host));
  }
  #endregion

}


