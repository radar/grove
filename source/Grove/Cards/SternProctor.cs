﻿namespace Grove.Cards
{
  using System.Collections.Generic;
  using Core;
  using Core.Ai;
  using Core.Details.Cards.Effects;
  using Core.Details.Cards.Triggers;
  using Core.Dsl;
  using Core.Targeting;
  using Core.Zones;

  public class SternProctor : CardsSource
  {
    public override IEnumerable<ICardFactory> GetCards()
    {
      yield return C.Card
        .Named("Stern Proctor")
        .ManaCost("{U}{U}")
        .Type("Creature Human Wizard")
        .Text(
          "When Stern Proctor enters the battlefield, return target artifact or enchantment to its owner's hand.")
        .FlavorText(
          "'I preferred the harsh tutors—they made mischief all the more fun.'{EOL}—Teferi, third-level student")
        .Power(1)
        .Toughness(2)
        .Timing(
          All(
            Timings.FirstMain(), 
            Timings.OpponentHasPermanent(card => card.Is().Artifact || card.Is().Enchantment)))
        .Abilities(
          C.TriggeredAbility(
            "When Stern Proctor enters the battlefield, return target artifact or enchantment to its owner's hand.",
            C.Trigger<OnZoneChange>((t, _) => t.To = Zone.Battlefield),
            C.Effect<ReturnToHand>(),
            C.Validator(Validators.Permanent(
              card => card.Is().Artifact || card.Is().Enchantment)),
            selectorAi: TargetSelectorAi.Bounce(),
            abilityCategory: EffectCategories.Bounce)
        );
    }
  }
}