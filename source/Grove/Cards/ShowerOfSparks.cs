﻿namespace Grove.Cards
{
  using System.Collections.Generic;
  using Core;
  using Core.Ai;
  using Core.Details.Cards.Effects;
  using Core.Dsl;
  using Core.Targeting;

  public class ShowerOfSparks : CardsSource
  {
    public override IEnumerable<ICardFactory> GetCards()
    {
      yield return C.Card
        .Named("Shower of Sparks")
        .ManaCost("{R}")
        .Type("Instant")
        .Timing(Timings.TargetRemovalInstant())
        .Text("Shower of Sparks deals 1 damage to target creature and 1 damage to target player.")
        .FlavorText("The viashino had learned how to operate the rig through trial and error—mostly error.")
        .Effect<DealDamageToTargets>(e => e.Amount = 1)
        .Targets(
          aiTargetSelector: TargetSelectorAi.DealDamageMultipleSelectors(amount: 1),
          effectValidators: new [] { C.Validator(Validators.Creature(), text: "Select target creature."), C.Validator(Validators.Player(), "Select target player.")}
        );
    }
  }
}