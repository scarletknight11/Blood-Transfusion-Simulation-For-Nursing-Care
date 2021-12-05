/* Distributed under the Apache License, Version 2.0.
   See accompanying NOTICE file for details.*/

using Pulse.CDM;

public enum PulseAction : int
{
  StartHemorrhage,
  StopHemorrhage,
  StartIVBag,
  StopIVBag,
  StartSuccinylcholineInfusion,
  StopSuccinylcholineInfusion,
  InjectMorphine,
  TensionPneumothorax,
  NeedleDecompressions,
  StartAirwayObstruction,
  StopAirwayObstruction,
  StartIntubation,
  StopIntubation,
  VentilateIntubation,
  VentilateMask
}

public class ExternalHemorrhageCmpt
{
  public static string RightArm { get { return "RightArm"; } }
  public static string LeftArm { get { return "LeftArm"; } }
  public static string RightLeg { get { return "RightLeg"; } }
  public static string LeftLeg { get { return "LeftLeg"; } }

  public static string LeftLung { get { return "LeftLung"; } }
  public static string RightLung { get { return "RightLung"; } }

  public static string Brain { get { return "Brain"; } }
  public static string Aorta { get { return "Aorta"; } }
  public static string VenaCava { get { return "VenaCava"; } }

  public static string RightKidney { get { return "RightKidney"; } }
  public static string LeftKidney { get { return "LeftKidney"; } }
  public static string Liver { get { return "Liver"; } }
  public static string Spleen { get { return "Spleen"; } }
  public static string Splanchnic { get { return "Splanchnic"; } }
  public static string SmallIntestine { get { return "SmallIntestine"; } }
  public static string LargeIntestine { get { return "LargeIntestine"; } }
}

public class InternalHemorrhageCmpt
{
  public static string RightKidney { get { return "RightKidney"; } }
  public static string LeftKidney { get { return "LeftKidney"; } }
  public static string Liver { get { return "Liver"; } }
  public static string Spleen { get { return "Spleen"; } }
  public static string Splanchnic { get { return "Splanchnic"; } }
  public static string SmallIntestine { get { return "SmallIntestine"; } }
  public static string LargeIntestine { get { return "LargeIntestine"; } }

  public static string Aorta { get { return "Aorta"; } }
  public static string VenaCava { get { return "VenaCava"; } }
}

public class Substance
{
  public static string Morphine { get { return "Morphine"; } }
  public static string Succinylcholine { get { return "Succinylcholine"; } }
}

public class Compound
{
  public static string Blood { get { return "Blood"; } }
  public static string Saline { get { return "Saline"; } }
  public static string PackedRBC { get { return "PackedRBC"; } }
}

public class PulseActionOnClick : PulseEngineController
{
  public PulseAction action;

  public void RunAction()
  {
    switch (action)
    {
      case PulseAction.StartHemorrhage:
        {
          SEHemorrhage h = new SEHemorrhage();
          h.SetCompartment(ExternalHemorrhageCmpt.RightLeg);
          h.SetType(SEHemorrhage.eType.External);
          h.GetSeverity().SetValue(0.3);
          driver.engine.ProcessAction(h);
          break;
        }
      case PulseAction.StopHemorrhage:
        {
          SEHemorrhage h = new SEHemorrhage();
          h.SetCompartment(ExternalHemorrhageCmpt.RightLeg);
          h.SetType(SEHemorrhage.eType.External);
          h.GetSeverity().SetValue(0);
          driver.engine.ProcessAction(h);
          break;
        }
      case PulseAction.StartIVBag:
        {
          SESubstanceCompoundInfusion sci = new SESubstanceCompoundInfusion();
          sci.SetSubstanceCompound(Compound.Saline);
          sci.GetBagVolume().SetValue(500, VolumeUnit.mL);
          sci.GetRate().SetValue(75, VolumePerTimeUnit.mL_Per_s);
          driver.engine.ProcessAction(sci);
          break;
        }
      case PulseAction.StopIVBag:
        {
          SESubstanceCompoundInfusion sci = new SESubstanceCompoundInfusion();
          sci.SetSubstanceCompound(Compound.Saline);
          sci.GetBagVolume().SetValue(0, VolumeUnit.mL);
          sci.GetRate().SetValue(0, VolumePerTimeUnit.mL_Per_s);
          driver.engine.ProcessAction(sci);
          break;
        }
      case PulseAction.StartSuccinylcholineInfusion:
        {
          SESubstanceInfusion si = new SESubstanceInfusion();
          si.SetSubstance(Substance.Succinylcholine);
          si.GetConcentration().SetValue(5000, MassPerVolumeUnit.ug_Per_mL);
          si.GetRate().SetValue(100, VolumePerTimeUnit.mL_Per_min);
          driver.engine.ProcessAction(si);
          break;
        }
      case PulseAction.StopSuccinylcholineInfusion:
        {
          SESubstanceInfusion si = new SESubstanceInfusion();
          si.SetSubstance(Substance.Succinylcholine);
          si.GetRate().SetValue(0, VolumePerTimeUnit.mL_Per_min);
          driver.engine.ProcessAction(si);
          break;
        }
      case PulseAction.InjectMorphine:
        {
          SESubstanceBolus bo = new SESubstanceBolus();
          bo.SetSubstance(Substance.Morphine);
          bo.GetConcentration().SetValue(10, MassPerVolumeUnit.mg_Per_mL);
          bo.GetDose().SetValue(1, VolumeUnit.mL);
          bo.SetAdminRoute(SESubstanceBolus.eAdministration.Intravenous);
          driver.engine.ProcessAction(bo);
          break;
        }
      case PulseAction.TensionPneumothorax:
        {
          SETensionPneumothorax tp = new SETensionPneumothorax();
          tp.SetSide(eSide.Left);
          tp.SetType(eGate.Open);
          tp.GetSeverity().SetValue(0.65);
          driver.engine.ProcessAction(tp);
          break;
        }
      case PulseAction.NeedleDecompressions:
        {
          SENeedleDecompression nd = new SENeedleDecompression();
          nd.SetSide(eSide.Left);
          nd.SetState(eSwitch.On);
          driver.engine.ProcessAction(nd);
          break;
        }
      case PulseAction.StartAirwayObstruction:
        {
          SEAirwayObstruction ao = new SEAirwayObstruction();
          ao.GetSeverity().SetValue(0.7);
          driver.engine.ProcessAction(ao);
          break;
        }
      case PulseAction.StopAirwayObstruction:
        {
          SEAirwayObstruction ao = new SEAirwayObstruction();
          ao.GetSeverity().SetValue(0.0);
          driver.engine.ProcessAction(ao);
          break;
        }
      case PulseAction.StartIntubation:
        {
          SEIntubation tub = new SEIntubation();
          tub.SetType(SEIntubation.eType.Tracheal);
          driver.engine.ProcessAction(tub);
          break;
        }
      case PulseAction.StopIntubation:
        {
          SEIntubation tub = new SEIntubation();
          tub.SetType(SEIntubation.eType.Off);
          driver.engine.ProcessAction(tub);
          break;
        }
      case PulseAction.VentilateIntubation:
        {
          SEAnesthesiaMachineConfiguration am = new SEAnesthesiaMachineConfiguration();
          am.GetConfiguration().SetConnection(SEAnesthesiaMachine.Connection.Tube);
          am.GetConfiguration().GetInletFlow().SetValue(5, VolumePerTimeUnit.L_Per_min);
          am.GetConfiguration().GetInspiratoryExpiratoryRatio().SetValue(0.5);
          am.GetConfiguration().GetOxygenFraction().SetValue(0.23);
          am.GetConfiguration().SetOxygenSource(SEAnesthesiaMachine.OxygenSource.Wall);
          am.GetConfiguration().GetPositiveEndExpiredPressure().SetValue(1, PressureUnit.cmH2O);
          am.GetConfiguration().SetPrimaryGas(SEAnesthesiaMachine.PrimaryGas.Nitrogen);
          am.GetConfiguration().GetRespiratoryRate().SetValue(16, FrequencyUnit.Per_min);
          am.GetConfiguration().GetPeakInspiratoryPressure().SetValue(10.5, PressureUnit.cmH2O);
          driver.engine.ProcessAction(am);
          break;
        }
      case PulseAction.VentilateMask:
        {
          SEAnesthesiaMachineConfiguration am = new SEAnesthesiaMachineConfiguration();
          am.GetConfiguration().SetConnection(SEAnesthesiaMachine.Connection.Mask);
          am.GetConfiguration().GetInletFlow().SetValue(5, VolumePerTimeUnit.L_Per_min);
          am.GetConfiguration().GetInspiratoryExpiratoryRatio().SetValue(0.5);
          am.GetConfiguration().GetOxygenFraction().SetValue(0.23);
          am.GetConfiguration().SetOxygenSource(SEAnesthesiaMachine.OxygenSource.Wall);
          am.GetConfiguration().GetPositiveEndExpiredPressure().SetValue(1, PressureUnit.cmH2O);
          am.GetConfiguration().SetPrimaryGas(SEAnesthesiaMachine.PrimaryGas.Nitrogen);
          am.GetConfiguration().GetRespiratoryRate().SetValue(16, FrequencyUnit.Per_min);
          am.GetConfiguration().GetPeakInspiratoryPressure().SetValue(10.5, PressureUnit.cmH2O);
          driver.engine.ProcessAction(am);
          break;
        }
    }
  }
}
