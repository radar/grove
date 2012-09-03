﻿namespace Grove.Cards
{
  using System.Collections.Generic;
  using Core;
  using Core.Ai;
  using Core.Details.Cards.Modifiers;
  using Core.Details.Mana;
  using Core.Dsl;

  public class DarkestHour : CardsSource
  {
    public override IEnumerable<ICardFactory> GetCards()
    {
      yield return C.Card
        .Named("Darkest Hour")
        .Type("Enchantment")
        .ManaCost("{B}")
        .Text("All creatures are black")
        .FlavorText("'Yawgmoth spent eons wrapping Phyrexians in human skin. They are the sleeper agents, and they are everywhere.'{EOL}—Xantcha, to Urza")
        .Timing(Timings.FirstMain())        
        .Abilities(
          C.Continuous((e, c) =>
            {
              e.CardFilter = (card, source) => card.Is().Creature;
              e.ModifierFactory = c.Modifier<SetColors>(
                (m, _) => { m.Colors = ManaColors.Black; });
            }));                
    }
  }
}