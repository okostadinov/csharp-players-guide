using FinalBattle;

Party partyA = new Party();
Party partyB = new Party();
partyA.Add(new Character("Skeleton"));
partyB.Add(new Character("Skeleton"));
new Battle(partyA, partyB).Start();