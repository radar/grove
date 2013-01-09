﻿namespace Grove.Cards
{
  using System.Collections.Generic;
  using Core;
  using Core.Ai;
  using Core.Cards;
  using Core.Cards.Effects;
  using Core.Cards.Modifiers;
  using Core.Cards.Triggers;
  using Core.Dsl;
  using Core.Targeting;
  using Core.Zones;

  public class Rancor : CardsSource
  {
    public override IEnumerable<ICardFactory> GetCards()
    {
      yield return Card
        .Named("Rancor")
        .ManaCost("{G}")
        .Type("Enchantment - Aura")
        .Text(
          "{Enchant creature}{EOL}Enchanted creature gets +2/+0 and has trample.{EOL}When Rancor is put into a graveyard from the battlefield, return Rancor to its owner's hand.")
        .FlavorText("Hatred outlives the hateful.")
        .Cast(p =>
          {
            p.Timing = Timings.FirstMain();
            p.Effect = Effect<Attach>(e => e.Modifiers(
              Modifier<AddPowerAndToughness>(m => m.Power = 2),
              Modifier<AddStaticAbility>(m => m.StaticAbility = Static.Trample)));
            p.EffectTargets = L(Target(Validators.Card(x => x.Is().Creature), Zones.Battlefield()));
            p.TargetSelectorAi = TargetSelectorAi.CombatEnchantment();
          })
        .Abilities(
          TriggeredAbility(
            "When Rancor is put into a graveyard from the battlefield, return Rancor to its owner's hand.",
            Trigger<OnZoneChanged>(t =>
              {
                t.From = Zone.Battlefield;
                t.To = Zone.Graveyard;
              }),
            Effect<PutToHand>(e => e.ReturnOwner = true)));
    }
  }
}