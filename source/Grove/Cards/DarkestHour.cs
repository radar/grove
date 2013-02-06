﻿namespace Grove.Cards
{
  using System.Collections.Generic;
  using Core;
  using Core.Ai.TimingRules;
  using Core.Dsl;
  using Core.Mana;
  using Core.Modifiers;

  public class DarkestHour : CardsSource
  {
    public override IEnumerable<CardFactory> GetCards()
    {
      yield return Card
        .Named("Darkest Hour")
        .Type("Enchantment")
        .ManaCost("{B}")
        .Text("All creatures are black")
        .FlavorText(
          "'Yawgmoth spent eons wrapping Phyrexians in human skin. They are the sleeper agents, and they are everywhere.'{EOL}—Xantcha, to Urza")
        .Cast(p => p.TimingRule(new FirstMain()))
        .ContinuousEffect(p =>
          {
            p.Modifier = () => new SetColors(ManaColors.Black);
            p.CardFilter = (card, source) => card.Is().Creature;
          });
    }
  }
}