#const width = 9.

% param("width",width).

dim(1..width).

tile((X,Y)) :- dim(X), dim(Y).
 
adj((X1,Y1),(X2,Y2)) :-
 sprite((X1,Y1),_),
 sprite((X2,Y2),_),
 |X1-X2| + |Y1-Y2| == 1.

start((1,1)).
finish((width,width)).

0 { sprite(T,path); sprite(T,boss); sprite(T,treasure); sprite(T,enemy)} 1 :- tile(T).
 
1 { sprite(T1,path): adj(T1,T) } 1 :- sprite(T,boss).

:- not sprite(T,path), start(T). 
:- not 1 { sprite(T1,boss): finish(T1)} 1.
:- not 1 { sprite(T,boss) } 1.
% :- not 3 { sprite(T,treasure) }.
% :- not width/2 { sprite(T,enemy) }.
:- not 3 { sprite(T,treasure) } width.
:- not width/2 { sprite(T,enemy) } width.

% cd C:\Users\PC\Documents //change to the corresponding ASP files folder path
% clingo -n 1 --rand-freq=1 --seed=%RANDOM% dungeon-core.lp dungeon-style.lp  //for one random dungeon
% python eval.py // for 1000 dungeons metrics results