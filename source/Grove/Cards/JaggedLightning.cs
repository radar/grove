﻿namespace Grove.Cards
{
  using System.Collections.Generic;
  using Core;
  using Core.Ai;
  using Core.Details.Cards.Effects;
  using Core.Dsl;
  using Core.Targeting;

  public class JaggedLightning : CardsSource
  {
    public override IEnumerable<ICardFactory> GetCards()
    {
      yield return C.Card
        .Named("Jagged Lightning")
        .ManaCost("{3}{R}{R}")
        .Type("Sorcery")
        .Timing(Timings.NoRestrictions())
        .Text("Jagged Lightning deals 3 damage to each of two target creatures.")
        .FlavorText(
          "The pungent smell of roasting flesh made both mages realize they'd rather break for dinner than fight.")
        .Effect<DealDamageToTargets>(e => e.Amount = 3)
        .Targets(
          aiTargetSelector: TargetSelectorAi.DealDamageSingleSelector(3),
          effectValidator: C.Validator(Validators.Creature(), minCount: 2, maxCount: 2)
        );
    }
  }
}