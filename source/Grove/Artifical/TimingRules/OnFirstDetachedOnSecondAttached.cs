﻿namespace Grove.Artifical.TimingRules
{
  using Gameplay.States;

  public class OnFirstDetachedOnSecondAttached : TimingRule
  {
    public override bool? ShouldPlay1(TimingRuleParameters p)
    {
      if (p.Card.IsAttached)
      {        
        return Turn.Step == Step.SecondMain;
      }
     
      return Turn.Step == Step.FirstMain;
    }
  }
}