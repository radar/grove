﻿namespace Grove.Cards
{
  using System.Collections.Generic;
  using Core;
  using Core.Ai;
  using Core.Details.Cards.Effects;
  using Core.Dsl;
  using Core.Targeting;

  public class Undermine : CardsSource
  {
    public override IEnumerable<ICardFactory> GetCards()
    {
      yield return C.Card
        .Named("Undermine")
        .ManaCost("{U}{U}{B}")
        .Type("Instant")
        .Text("Counter target spell. Its controller loses 3 life.")
        .FlavorText("'Which would you like first, the insult or the injury?'")
        .Category(EffectCategories.Counterspell)
        .Timing(Timings.CounterSpell())
        .Effect<CounterTargetSpell>(e => e.ControllersLifeloss = 3)
        .Targets(
          aiTargetSelector: TargetSelectorAi.CounterSpell(),
          effectValidator: C.Validator(Validators.Counterspell()));
    }
  }
}