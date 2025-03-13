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
using Cognex.VisionPro.Blob;
#endregion

public class CogToolBlockAdvancedScript : CogToolBlockAdvancedScriptBase
{
  #region Private Member Variables
  private Cognex.VisionPro.ToolBlock.CogToolBlock mToolBlock;
  private   CogBlobTool CogBlobTool1;
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
      mToolBlock.Outputs["Result"].Value = false;
    foreach(ICogTool tool in mToolBlock.Tools)
      mToolBlock.RunTool(tool, ref message, ref result);
    double Area = 0;
    CogBlobTool1 = (CogBlobTool) mToolBlock.Tools["CogBlobTool1"];
    if(CogBlobTool1.Results.GetBlobs().Count > 0)
    {
      for(int i = 0;i < CogBlobTool1.Results.GetBlobs().Count;i++)
      {
      
        Area += CogBlobTool1.Results.GetBlobs()[i].Area;
        
      }
    
    }
    if(Area < (double) mToolBlock.Inputs["BlobAreaUpLimit"].Value)
    {
    
      mToolBlock.Outputs["Result"].Value = true;
    }
    else
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
    this.mToolBlock = ((Cognex.VisionPro.ToolBlock.CogToolBlock)(host));
  }
  #endregion

}








xw

#region namespace imports
using System;
using System.Collections;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Windows.Forms;
using Cognex.VisionPro;
using Cognex.VisionPro.ToolBlock;
using Cognex.VisionPro3D;
using Cognex.VisionPro.ImageProcessing;
using Cognex.VisionPro.CalibFix;
using Cognex.VisionPro.PMAlign;
#endregion

public class CogToolBlockAdvancedScript : CogToolBlockAdvancedScriptBase
{
  #region Private Member Variables
  private Cognex.VisionPro.ToolBlock.CogToolBlock mToolBlock;
  private Cognex.VisionPro.CogGraphicCollection mCogGraphicCollection = new CogGraphicCollection();
  //private List<ICogGraphic> mCogGraphicCollection = new List<ICogGraphic>();

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
    mCogGraphicCollection.Clear();
    mToolBlock.Outputs["Result"].Value = true;
    string mes = "";
    
    mToolBlock.Outputs[1].Value = 999;
    mToolBlock.Outputs[2].Value = 999;
    mToolBlock.Outputs[3].Value = 999;
    mToolBlock.Outputs[4].Value = 999;
    mToolBlock.Outputs[5].Value = 999;
    
    
    double max1 = Convert.ToDouble(mToolBlock.Inputs["Up_Max"].Value);
    double min1 = Convert.ToDouble(mToolBlock.Inputs["Up_Min"].Value);
    double max2 = Convert.ToDouble(mToolBlock.Inputs["Range_Max"].Value);
    double min2 = Convert.ToDouble(mToolBlock.Inputs["Range_Min"].Value);
    double max3 = Convert.ToDouble(mToolBlock.Inputs["Left_Max"].Value);
    double min3 = Convert.ToDouble(mToolBlock.Inputs["Left_Min"].Value);
    // Run each tool using the RunTool function
    foreach(ICogTool tool in mToolBlock.Tools)
    {
      try 
      {	  
        mToolBlock.RunTool(tool, ref message, ref result);
      }
      catch (Exception)
      {
        mToolBlock.Outputs["Result"].Value = false;
        mToolBlock.Outputs["Message"].Value = "测试方案运行异常";
        return false;
      }
    }
    
    CogToolBlock tb1 = mToolBlock.Tools["左右间距"] as CogToolBlock;
    CogToolBlock tb2 = mToolBlock.Tools["上下间距"] as CogToolBlock;
    CogToolBlock tb3 = mToolBlock.Tools["左间距"] as CogToolBlock;
    CogToolBlock tb4 = mToolBlock.Tools["掉落"] as CogToolBlock;
    mToolBlock.Outputs[1].Value = tb2.Outputs[0].Value;
    mToolBlock.Outputs[2].Value = tb2.Outputs[1].Value;
    mToolBlock.Outputs[3].Value = tb1.Outputs[0].Value;
    mToolBlock.Outputs[4].Value = tb1.Outputs[1].Value;
    mToolBlock.Outputs[5].Value = tb3.Outputs[0].Value;
    
    
    double min = Convert.ToDouble(tb2.Outputs["Min"].Value);
    double max = Convert.ToDouble(tb2.Outputs["Max"].Value);
    
    
    if (!(bool) tb4.Outputs["Result"].Value) {
      mes += "  限位片掉落  ";
      mToolBlock.Outputs["Result"].Value = false;
    }
    
    
    if (min < min1 || max > max1)
    { 
      mes += "  上下间距异常  ";
      mToolBlock.Outputs["Result"].Value = false;
    }
    
    min = Convert.ToDouble(tb1.Outputs["Min"].Value);
    max = Convert.ToDouble(tb1.Outputs["Max"].Value);
    
    if (min < min2 || max > max2)
    { 
      mes += "  左右间距异常  ";
      mToolBlock.Outputs["Result"].Value = false;
    }
    
    min = Convert.ToDouble(tb3.Outputs["Min"].Value);
    max = Convert.ToDouble(tb3.Outputs["Max"].Value);
    
    if (min < min3 || max > max3)
    { 
      mes += "  右间距异常  ";
      mToolBlock.Outputs["Result"].Value = false;
    }
    show(mes);
    mToolBlock.Outputs["Message"].Value = mes;
    return false;
  }

  void show(string message)
  {
    CogGraphicLabel myLabel = new CogGraphicLabel();
    myLabel.SelectedSpaceName = "#";
    myLabel.Color = CogColorConstants.Red;
    myLabel.Font = new Font("宋体", 16);
    myLabel.SetXYText(2222, 666, message);
    myLabel.Alignment = CogGraphicLabelAlignmentConstants.BottomLeft;
    mCogGraphicCollection.Add(myLabel);
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
    
    foreach (ICogGraphic mGraphic in mCogGraphicCollection)
    {
     // mToolBlock.AddGraphicToRunRecord(mGraphic, lastRecord, "CogImageConvertTool1.InputImage", "");
      mToolBlock.AddGraphicToRunRecord(mGraphic, lastRecord, lastRecord.SubRecords[0].RecordKey, "");
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


sx

#region namespace imports
using System;
using System.Collections;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Windows.Forms;
using Cognex.VisionPro;
using Cognex.VisionPro.ToolBlock;
using Cognex.VisionPro3D;
using Cognex.VisionPro.Caliper;
#endregion
public class MyPoint
{
  double X = 0;
  double Y = 0;
  public MyPoint(double x, double y)
  {
    X = x;
    Y = y;
  }
}
public class CogToolBlockAdvancedScript : CogToolBlockAdvancedScriptBase
{
  #region Private Member Variables
  private Cognex.VisionPro.ToolBlock.CogToolBlock mToolBlock;
      List<ICogGraphic> list = new List<ICogGraphic>();
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
    list.Clear();
    ICogImage image = mToolBlock.Inputs[0].Value as  ICogImage;
    CogLine Line = null;
    double max = -99;
    double min = 99;
    
    double max1 = Convert.ToDouble(mToolBlock.Inputs["Range_Max"].Value);
    double min1 = Convert.ToDouble(mToolBlock.Inputs["Range_Min"].Value);
    // Run each tool using the RunTool function
    foreach(ICogTool tool in mToolBlock.Tools)
    {
      try 
      {	      
        mToolBlock.RunTool(tool, ref message, ref result);
        if (tool.Name.Contains("基准"))
        {
          CogFindLineTool fl1 = tool as CogFindLineTool;
          if (tool.RunStatus.Result != CogToolResultConstants.Accept || 
            fl1.Results.NumPointsUsed < fl1.RunParams.NumCalipers * 0.6)
          {
            mToolBlock.Outputs["Min"].Value = -999;
            mToolBlock.Outputs["Max"].Value = 999;
            return false;
          }
          else
          {
            Line = fl1.Results.GetLine();
          }
        }
        if (tool.Name.Contains("测量"))
        {
          CogFindLineTool fl2 = tool as CogFindLineTool;
          if (tool.RunStatus.Result != CogToolResultConstants.Accept || 
            fl2.Results.NumPointsUsed < fl2.RunParams.NumCalipers * 0.6 || Line == null)
          {
            mToolBlock.Outputs["Min"].Value = -999;
            mToolBlock.Outputs["Max"].Value = 999;
            return false;
          }
          else
          {
            for (int i = 0; i < fl2.Results.Count; i++)
            {
              if(fl2.Results[i].Used)
              {
                double x,y;
                double dis = CogMath.DistancePointLine(fl2.Results[i].X, fl2.Results[i].Y, Line, image, out x, out y);
                if (dis < min)
                {
                  min = dis;
                }
                if (dis > max)
                {
                  max = dis;
                }
                if (dis < min1 || dis > max1)
                {
                  AddLabel(x, y, dis.ToString("0.00"), false, fl2.InputImage.SelectedSpaceName);
                }
                else
                {
                  AddLabel(x, y, dis.ToString("0.00"), true, fl2.InputImage.SelectedSpaceName);
                }
              }
            }
          }
        }
      }
      catch (Exception)
      {
        mToolBlock.Outputs["Min"].Value = -999;
        mToolBlock.Outputs["Max"].Value = 999;
        return false;
      }
    }
    mToolBlock.Outputs["Max"].Value = max;
    mToolBlock.Outputs["Min"].Value = min;
    
    
    return false;
  }
  public void  AddLabel(double x, double y, string text, bool Re, string sapcename)
  {
    CogGraphicLabel label = new CogGraphicLabel();
    label.SetXYText(x, y, text);
    label.Color = Re ? CogColorConstants.Green : CogColorConstants.Red;
    label.Rotation = CogMisc.DegToRad(0);
    label.Alignment = CogGraphicLabelAlignmentConstants.BottomLeft;
    label.SelectedSpaceName = sapcename;
    label.Font = new Font("微软雅黑", 10);
    list.Add(label);
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
    
    foreach ( var item in list)
    {
      mToolBlock.AddGraphicToRunRecord(item, lastRecord, lastRecord.SubRecords[0].RecordKey, "");
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


