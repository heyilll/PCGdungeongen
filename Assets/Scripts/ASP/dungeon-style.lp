:- not 8*(width*width)/10 { sprite(T,_) }.
% :- not 5*(width*width)/10 { sprite(T,_) }.

1 { sprite(T2,path): adj(T1,T2) } 3 :- sprite(T1,path).
 
1 { sprite(T2,path): adj(T1,T2) } :- sprite(T1,_), not sprite(T1,path).

0 { sprite(T2,enemy): adj(T1,T2) } 3 :- sprite(T1,enemy).

