﻿namespace Grove.Gameplay.CastingRules
{
  using System;
  using States;

  public class Sorcery : CastingRule
  {
    private readonly Action<Card> _afterResolvePutToZone;

    private Sorcery() {}

    public Sorcery(Action<Card> afterResolvePutToZone = null)
    {
      _afterResolvePutToZone = afterResolvePutToZone ?? (card => card.PutToGraveyard());
    }

    public override bool CanCast()
    {
      return Turn.Step.IsMain() &&
        Card.Controller.IsActive &&
          Stack.IsEmpty;
    }

    public override void AfterResolve()
    {
      _afterResolvePutToZone(Card);
    }
  }
}