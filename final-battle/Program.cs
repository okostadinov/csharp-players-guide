using FinalBattle;

Party partyA = new Party(PartyType.Player);
Party partyB = new Party(PartyType.Computer);
partyA.Add(new TrueProgrammer("TOG"));
partyB.Add(new Skeleton());
new Battle(partyA, partyB).Start();