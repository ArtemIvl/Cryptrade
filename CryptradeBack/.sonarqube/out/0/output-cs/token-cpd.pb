ö
c/Users/artemivliev/Artem/Cryptrade/CryptradeBack/JwtAuthenticationManager/CustomJwtAuthExtension.cs
	namespace 	$
JwtAuthenticationManager
 "
{ 
public 
static 
class "
CustomJwtAuthExtension +
{		 
public

 
static

	 
void

 &
AddCustomJwtAuthentication

 /
(

/ 0
this

0 4
IServiceCollection

5 G
services

H P
)

P Q
{ 
services 
. 
AddAuthentication 
( 
o 
=>  "
{ 
o 
. %
DefaultAuthenticateScheme 
=  !
JwtBearerDefaults" 3
.3 4 
AuthenticationScheme4 H
;H I
o 
. "
DefaultChallengeScheme 
= 
JwtBearerDefaults 0
.0 1 
AuthenticationScheme1 E
;E F
} 
) 
. 
AddJwtBearer 
( 
o 
=> 
{ 
o 
.  
RequireHttpsMetadata 
= 
false "
;" #
o 
. 
	SaveToken 
= 
true 
; 
o 
. %
TokenValidationParameters 
=  !
new" %%
TokenValidationParameters& ?
{ $
ValidateIssuerSigningKey 
= 
true  $
,$ %
ValidateIssuer 
= 
false 
, 
ValidateAudience 
= 
false 
, 
IssuerSigningKey 
= 
new  
SymmetricSecurityKey 0
(0 1
Encoding1 9
.9 :
ASCII: ?
.? @
GetBytes@ H
(H I
JwtTokenHandlerI X
.X Y
JWT_SECURITY_KEYY i
)i j
)j k
} 
; 
} 
) 
; 
} 
} 
} ¡
X/Users/artemivliev/Artem/Cryptrade/CryptradeBack/JwtAuthenticationManager/Entity/User.cs
	namespace 	$
JwtAuthenticationManager
 "
." #
Entity# )
{ 
public 
class 
User 
{ 
public 
int	 
id 
{ 
get 
; 
set 
; 
} 
public 
string 
name 
{ 
get  
;  !
set" %
;% &
}' (
public 
string 
email 
{ 
get !
;! "
set# &
;& '
}( )
public		 
string		 
password		 
{		  
get		! $
;		$ %
set		& )
;		) *
}		+ ,
public

 
string

 
role

 
{

 
get

  
;

  !
set

" %
;

% &
}

' (
} 
} •.
\/Users/artemivliev/Artem/Cryptrade/CryptradeBack/JwtAuthenticationManager/JwtTokenHandler.cs
	namespace		 	$
JwtAuthenticationManager		
 "
{

 
public 
class 
JwtTokenHandler 
{ 
public 
const	 
string 
JWT_SECURITY_KEY &
=' (
$str) S
;S T
private 	
const
 
int #
JWT_TOKEN_VALIDITY_MINS +
=, -
$num. 0
;0 1
public 
JwtTokenHandler 
( 
)  
{ 	
} 	
public "
AuthenticationResponse %
?% &
GenerateJwtToken' 7
(7 8!
AuthenticationRequest8 M!
authenticationRequestN c
,c d
Liste i
<i j
Userj n
>n o
usersp u
)u v
{ 
if 
( 
string 
. 
IsNullOrWhiteSpace  
(  !!
authenticationRequest! 6
.6 7
email7 <
)< =
||> @
stringA G
.G H
IsNullOrWhiteSpaceH Z
(Z [!
authenticationRequest[ p
.p q
passwordq y
)y z
)z {
return 

null 
; 
var 
userAccount 
= 
users 
. 
FirstOrDefault )
() *
u* +
=>, .
u/ 0
.0 1
email1 6
==7 9!
authenticationRequest: O
.O P
emailP U
)U V
;V W
if 
( 
userAccount 
== 
null 
|| 
! 
BCrypt %
.% &
Net& )
.) *
BCrypt* 0
.0 1
Verify1 7
(7 8!
authenticationRequest8 M
.M N
passwordN V
,V W
userAccountX c
.c d
passwordd l
)l m
)m n
{ 
return   

null   
;   
}!! 
var##  
tokenExpiryTimeStamp## 
=## 
DateTime## &
.##& '
Now##' *
.##* +

AddMinutes##+ 5
(##5 6#
JWT_TOKEN_VALIDITY_MINS##6 M
)##M N
;##N O
var$$ 
tokenKey$$ 
=$$ 
Encoding$$ 
.$$ 
ASCII$$  
.$$  !
GetBytes$$! )
($$) *
JWT_SECURITY_KEY$$* :
)$$: ;
;$$; <
var%% 
claimsIdentity%% 
=%% 
new%% 
ClaimsIdentity%% *
(%%* +
new%%+ .
List%%/ 3
<%%3 4
Claim%%4 9
>%%9 :
{&& 
new'' 
Claim'' 
('' #
JwtRegisteredClaimNames'' %
.''% &
Email''& +
,''+ ,!
authenticationRequest''- B
.''B C
email''C H
)''H I
,''I J
new(( 
Claim(( 
((( #
JwtRegisteredClaimNames(( %
.((% &
Name((& *
,((* +
userAccount((, 7
.((7 8
name((8 <
)((< =
,((= >
new)) 
Claim)) 
()) 

ClaimTypes)) 
.)) 
NameIdentifier)) '
,))' (
userAccount))) 4
.))4 5
id))5 7
.))7 8
ToString))8 @
())@ A
)))A B
)))B C
,))C D
new** 
Claim** 
(** 

ClaimTypes** 
.** 
Role** 
,** 
userAccount** *
.*** +
role**+ /
)**/ 0
,**0 1
},, 
),, 
;,, 
var.. 
signingCredentials.. 
=.. 
new.. 
SigningCredentials..  2
(..2 3
new//  
SymmetricSecurityKey// 
(// 
tokenKey// %
)//% &
,//& '
SecurityAlgorithms00 
.00 
HmacSha256Signature00 *
)00* +
;00+ ,
var22 #
securityTokenDescriptor22 
=22  
new22! $#
SecurityTokenDescriptor22% <
{33 
Subject44 
=44 
claimsIdentity44 
,44 
Expires55 
=55  
tokenExpiryTimeStamp55 "
,55" #
SigningCredentials66 
=66 
signingCredentials66 +
}77 
;77 
var99 #
jwtSecurityTokenHandler99 
=99  
new99! $#
JwtSecurityTokenHandler99% <
(99< =
)99= >
;99> ?
var:: 
securityToken:: 
=:: #
jwtSecurityTokenHandler:: .
.::. /
CreateToken::/ :
(::: ;#
securityTokenDescriptor::; R
)::R S
;::S T
var;; 
token;; 
=;; #
jwtSecurityTokenHandler;; &
.;;& '

WriteToken;;' 1
(;;1 2
securityToken;;2 ?
);;? @
;;;@ A
return== 	
new==
 "
AuthenticationResponse== $
{>> 
email?? 	
=??
 
userAccount?? 
.?? 
email?? 
,?? 
	expiresIn@@ 
=@@ 
(@@ 
int@@ 
)@@  
tokenExpiryTimeStamp@@ )
.@@) *
Subtract@@* 2
(@@2 3
DateTime@@3 ;
.@@; <
Now@@< ?
)@@? @
.@@@ A
TotalSeconds@@A M
,@@M N
jwtTokenAA 
=AA 
tokenAA 
}BB 
;BB 
}CC 
}DD 
}EE €
i/Users/artemivliev/Artem/Cryptrade/CryptradeBack/JwtAuthenticationManager/Models/AuthenticationRequest.cs
	namespace 	$
JwtAuthenticationManager
 "
." #
Models# )
{ 
public 
class !
AuthenticationRequest #
{ 
public 
string	 
email 
{ 
get 
; 
set  
;  !
}" #
public 
string	 
password 
{ 
get 
; 
set  #
;# $
}% &
} 
}		 œ
j/Users/artemivliev/Artem/Cryptrade/CryptradeBack/JwtAuthenticationManager/Models/AuthenticationResponse.cs
	namespace 	$
JwtAuthenticationManager
 "
." #
Models# )
{ 
public 
class "
AuthenticationResponse $
{ 
public 
string	 
email 
{ 
get 
; 
set  
;  !
}" #
public 
string	 
jwtToken 
{ 
get 
; 
set  #
;# $
}% &
public 
int	 
	expiresIn 
{ 
get 
; 
set !
;! "
}# $
}		 
}

 ¡
T/Users/artemivliev/Artem/Cryptrade/CryptradeBack/JwtAuthenticationManager/Program.cs
var 
builder 
= 
WebApplication 
. 
CreateBuilder *
(* +
args+ /
)/ 0
;0 1
builder 
. 
Configuration 
. 
AddJsonFile !
(! "
$str" 4
)4 5
;5 6
var 
app 
= 	
builder
 
. 
Build 
( 
) 
; 
app 
. 
Run 
( 
) 	
;	 
