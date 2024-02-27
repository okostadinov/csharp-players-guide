using FinalBattle;

Party partyA = new Party(PartyType.Player);
Party partyB = new Party(PartyType.Computer);
TrueProgrammer player = Game.GeneratePlayerCharacter();
partyA.Add(player);
partyB.Add(new Skeleton());
partyB.Add(new Skeleton());
new Battle(partyA, partyB).Start();