﻿namespace Grove.Gameplay.Costs
{
  using System;
  using Targeting;

  public class DiscardThis : Cost
  {
    protected override void CanPay(CanPayResult result)
    {
      result.CanPay(true);
    }

    protected override void PayCost(Targets targets, int? x, int repeat)
    {
      Card.Discard();
    }
  }
}