# espill

Her er mine spill. 

Jeg fant jo ut nå at github kan gi dere en automagisk .gitignore fil for unity, noe som dere allerede har funnet ut at dere trengte på den harde måten. Sjekk min .gitignore, den har jeg ikke laget sjøl.


## Builds
Jeg har ei mappe som heter Builds, dette lærte jeg av unity sine egne youtube tutorials... Der er det ei mappe for windows og ei for linux. Jeg er ganske sikker på at grunnen at det har vært litt hanglete når jeg har prøvd å kjøre deres builds har vært fordi dere har bygget for windows. Den kjørbare fila skal ha suffix .x86_64 og ikke .exe.

# FallendeBaller
Dette er mitt eneste "spill" så langt. Det er ikke interaktivt, så det kvalifiserer ikke helt som spill. Men, koden og scripta skal brukes til å lage et fotballspill seinere.

Jeg har fire baller, hver styres av fire forskjellige script. De kaller igjen fire forskjellige script som er kopiert fra bokas kode, og bare modifisert litt.

### SimpleBall
Denne styres av bokas SimpleProjectileODE klasse. Det er bare tyngdekraft og ikke noen luftmotstand.

I alle disse scripta måtte jeg gå gjennom og forandre fra double til float. Det går sikkert an å bruke double, men jeg gjorde det nå slik. Vi trenger vel stort sett ikke dobbelpresijon i et spill.

Jeg gikk også gjennom og la til en f etter alle talla, f.eks. 0.02 -> 0.02f

I tillegg så bytter jeg om z og y når jeg kaller disse funksjonene fra boka, siden unity har y i vertikal retning, mens boka sine script har z som vertikal retning.

### DragBall
Denne styres av dragProjectile. Her er det altså luftmotstand. Husk z byttes mot y.

### WindBall
Her er det også vind i x- og z-retning.

### SpinBall
Denne spinner rundt x-aksen, og får en bitteliten Magnuskraft på seg. Ser dere noe morro her? Dette er den ballen som aldri lander :) Jeg satt den til å rotere med \omega = 100 000 rad/s, for å få magnuskrafta til å få en effekt, og vi får da en veldig morsom oppførsel på ballen.

Hvorfor er oppførselen til ballen ufysisk?

## Forbedringspotensiale

Det er helt klart mange ting som kan forbedres.

* Legge til knapper og kontrollmuligheter, lage et spill ut av det med andre ord.

* Bruke Vector3 i stedet for x,y,z,vx,vy,vz ...

Men, fysikken fungerer, og Runge-Kutta løseren fungerer. Så, for ny fysikk så kan jeg da lage en klasse som arver ODE, og hvor jeg implementerer en ny getRightHandSide().
