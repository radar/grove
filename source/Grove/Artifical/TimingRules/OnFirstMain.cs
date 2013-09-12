﻿namespace Grove.Artifical.TimingRules
{
  using Gameplay.States;

  public class OnFirstMain : TimingRule
  {
    public override bool? ShouldPlay1(TimingRuleParameters p)
    {
      return p.Controller.IsActive && Turn.Step == Step.FirstMain && Stack.IsEmpty;
    }
  }
}