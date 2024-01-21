¯=
k/Users/artemivliev/Artem/Cryptrade/CryptradeBack/TransactionManagement/Controllers/TransactionController.cs
	namespace 	!
TransactionManagement
 
.  
Controllers  +
{ 
[ 
Route 

(
 
$str 
) 
] 
[		 
ApiController		 
]		 
public

 

class

 !
TransactionController

 &
:

' (
ControllerBase

) 7
{ 
private 
readonly 
TransactionService +
_transactionService, ?
;? @
public !
TransactionController $
($ %
TransactionService% 7
transactionService8 J
)J K
{ 	
_transactionService 
=  !
transactionService" 4
;4 5
} 	
[ 	
HttpPost	 
] 
public 
IActionResult 
CreateTransaction .
(. /
[/ 0
FromBody0 8
]8 9
Transaction: E
modelF K
)K L
{ 	
try 
{ 
_transactionService #
.# $
AddTransaction$ 2
(2 3
model3 8
)8 9
;9 :
return 
Ok 
( 
$str /
)/ 0
;0 1
} 
catch 
( 
	Exception 
ex 
)  
{ 
return 

BadRequest !
(! "
$"" $
$str$ 9
{9 :
ex: <
.< =
Message= D
}D E
"E F
)F G
;G H
} 
}   	
["" 	
HttpGet""	 
]"" 
public## 
async## 
Task## 
<## 
IActionResult## '
>##' (
GetAllTransactions##) ;
(##; <
int##< ?
portfolioId##@ K
)##K L
{$$ 	
try%% 
{&& 
var'' 
transactions''  
=''! "
await''# (
_transactionService'') <
.''< =(
GetTransactionsByPortfolioId''= Y
(''Y Z
portfolioId''Z e
)''e f
;''f g
return(( 
Ok(( 
((( 
transactions(( &
)((& '
;((' (
})) 
catch** 
(** 
	Exception** 
ex** 
)**  
{++ 
return,, 

BadRequest,, !
(,,! "
$",," $
$str,,$ 9
{,,9 :
ex,,: <
.,,< =
Message,,= D
},,D E
",,E F
),,F G
;,,G H
}-- 
}.. 	
[00 	
HttpGet00	 
(00 
$str00 
)00 
]00 
public11 
async11 
Task11 
<11 
IActionResult11 '
>11' (
GetAllAssets11) 5
(115 6
int116 9
portfolioId11: E
)11E F
{22 	
try33 
{44 
var55 
assets55 
=55 
await55 "
_transactionService55# 6
.556 7"
GetAssetsByPortfolioId557 M
(55M N
portfolioId55N Y
)55Y Z
;55Z [
return66 
Ok66 
(66 
assets66  
)66  !
;66! "
}77 
catch88 
(88 
	Exception88 
ex88 
)88  
{99 
return:: 

BadRequest:: !
(::! "
$"::" $
$str::$ 9
{::9 :
ex::: <
.::< =
Message::= D
}::D E
"::E F
)::F G
;::G H
};; 
}<< 	
[>> 	
HttpGet>>	 
(>> 
$str>> 
)>> 
]>>  
public?? 
async?? 
Task?? 
<?? 
IActionResult?? '
>??' ("
GetTransactionsByAsset??) ?
(??? @
int??@ C
portfolioId??D O
,??O P
string??Q W
	assetName??X a
)??a b
{@@ 	
tryAA 
{BB 
varCC 
transactionsCC  
=CC! "
awaitCC# (
_transactionServiceCC) <
.CC< ="
GetTransactionsByAssetCC= S
(CCS T
portfolioIdCCT _
,CC_ `
	assetNameCCa j
)CCj k
;CCk l
returnDD 
OkDD 
(DD 
transactionsDD &
)DD& '
;DD' (
}EE 
catchFF 
(FF 
	ExceptionFF 
exFF 
)FF  
{GG 
returnHH 

BadRequestHH !
(HH! "
$"HH" $
$strHH$ 9
{HH9 :
exHH: <
.HH< =
MessageHH= D
}HHD E
"HHE F
)HHF G
;HHG H
}II 
}JJ 	
[LL 	

HttpDeleteLL	 
]LL 
publicMM 
IActionResultMM 
DeleteTransactionMM .
(MM. /
intMM/ 2
transactionIdMM3 @
)MM@ A
{NN 	
tryOO 
{PP 
_transactionServiceQQ #
.QQ# $
DeleteTransactionQQ$ 5
(QQ5 6
transactionIdQQ6 C
)QQC D
;QQD E
returnRR 
OkRR 
(RR 
$strRR <
)RR< =
;RR= >
}SS 
catchTT 
(TT 
	ExceptionTT 
exTT 
)TT  
{UU 
returnVV 

BadRequestVV !
(VV! "
$"VV" $
$strVV$ B
{VVB C
exVVC E
.VVE F
MessageVVF M
}VVM N
"VVN O
)VVO P
;VVP Q
}WW 
}XX 	
[[[ 	

HttpDelete[[	 
([[ 
$str[[ !
)[[! "
][[" #
public\\ 
IActionResult\\ *
DeleteTransactionByPortfolioId\\ ;
(\\; <
int\\< ?
portfolioId\\@ K
)\\K L
{]] 	
try^^ 
{__ 
_transactionService`` #
.``# $+
DeleteTransactionsByPortfolioId``$ C
(``C D
portfolioId``D O
)``O P
;``P Q
returnaa 
Okaa 
(aa 
$straa <
)aa< =
;aa= >
}bb 
catchcc 
(cc 
	Exceptioncc 
excc 
)cc  
{dd 
returnee 

BadRequestee !
(ee! "
$"ee" $
$stree$ B
{eeB C
exeeC E
.eeE F
MessageeeF M
}eeM N
"eeN O
)eeO P
;eeP Q
}ff 
}gg 	
[ii 	
HttpPutii	 
]ii 
publicjj 
IActionResultjj !
UpdateTransactionDatajj 2
(jj2 3
[jj3 4
FromBodyjj4 <
]jj< = 
TransactionDataModeljj> R
modeljjS X
,jjX Y
intjjZ ]
transactionIdjj^ k
)jjk l
{kk 	
tryll 
{mm 
_transactionServicenn #
.nn# $
UpdateTransactionnn$ 5
(nn5 6
modelnn6 ;
,nn; <
transactionIdnn= J
)nnJ K
;nnK L
returnoo 
Okoo 
(oo 
$stroo A
)ooA B
;ooB C
}pp 
catchqq 
(qq 
	Exceptionqq 
exqq 
)qq  
{rr 
returnss 

BadRequestss !
(ss! "
$"ss" $
$strss$ G
{ssG H
exssH J
.ssJ K
MessagessK R
}ssR S
"ssS T
)ssT U
;ssU V
}tt 
}uu 	
}vv 
}ww ñ
c/Users/artemivliev/Artem/Cryptrade/CryptradeBack/TransactionManagement/Data/TransactionDbContext.cs
	namespace 	!
TransactionManagement
 
.  
Data  $
{ 
public		 
class		  
TransactionDbContext		 "
:		# $
	DbContext		% .
{

 
public  
TransactionDbContext #
(# $
DbContextOptions$ 4
<4 5 
TransactionDbContext5 I
>I J
optionsK R
)R S
:T U
baseV Z
(Z [
options[ b
)b c
{ 	
try 
{ 
var 
databaseCreator #
=$ %
Database& .
.. /

GetService/ 9
<9 :
IDatabaseCreator: J
>J K
(K L
)L M
asN P%
RelationalDatabaseCreatorQ j
;j k
if 
( 
databaseCreator #
!=$ &
null' +
)+ ,
{ 
if 
( 
! 
databaseCreator (
.( )

CanConnect) 3
(3 4
)4 5
)5 6
databaseCreator7 F
.F G
CreateG M
(M N
)N O
;O P
if 
( 
! 
databaseCreator (
.( )
	HasTables) 2
(2 3
)3 4
)4 5
databaseCreator6 E
.E F
CreateTablesF R
(R S
)S T
;T U
} 
} 
catch 
( 
	Exception 
ex 
)  
{ 
throw 
new 
	Exception #
(# $
ex$ &
.& '
ToString' /
(/ 0
)0 1
)1 2
;2 3
} 
} 	
public 
DbSet 
< 
Transaction  
>  !
Transactions" .
{/ 0
get1 4
;4 5
set6 9
;9 :
}; <
} 
} œ
V/Users/artemivliev/Artem/Cryptrade/CryptradeBack/TransactionManagement/Entity/Asset.cs
	namespace 	!
TransactionManagement
 
.  
Entity  &
{ 
public 
class 
Asset 
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
public 
string	 

cryptoName 
{ 
get  
;  !
set" %
;% &
}' (
public 
string	 
cryptoSymbol 
{ 
get "
;" #
set$ '
;' (
}) *
public		 
double			 
avgBuyPrice		 
{		 
get		 !
;		! "
set		# &
;		& '
}		( )
public

 
double

	 
amount

 
{

 
get

 
;

 
set

 !
;

! "
}

# $
public 
int	 
numOfTransactions 
{  
get! $
;$ %
set& )
;) *
}+ ,
public 
double	 
percentChange24h  
{! "
get# &
;& '
set( +
;+ ,
}- .
public 
double	 
currentPrice 
{ 
get "
;" #
set$ '
;' (
}) *
public 
double	 

profitLoss 
{ 
get  
;  !
set" %
;% &
}' (
public 
int	 
portfolioId 
{ 
get 
; 
set  #
;# $
}% &
} 
} ®
\/Users/artemivliev/Artem/Cryptrade/CryptradeBack/TransactionManagement/Entity/Transaction.cs
	namespace 	!
TransactionManagement
 
.  
Entity  &
{ 
[ 
Table 
( 
$str 
) 
] 
public 
class 
Transaction 
{		 
[

 
Key

 
]

 
[ 
Column 	
(	 

$str
 
) 
] 
public 
int	 
id 
{ 
get 
; 
set 
; 
} 
[ 	
Column	 
( 
$str 
) 
] 
public 
DateTime 
	createdAt !
{" #
get$ '
;' (
set) ,
;, -
}. /
[ 	
Column	 
( 
$str 
) 
] 
public 
string 

cryptoName  
{! "
get# &
;& '
set( +
;+ ,
}- .
[ 	
Column	 
( 
$str 
) 
]  
public 
string 
cryptoSymbol "
{# $
get% (
;( )
set* -
;- .
}/ 0
[ 	
Column	 
( 
$str 
) 
] 
public 
string 
type 
{ 
get  
;  !
set" %
;% &
}' (
[ 	
Column	 
( 
$str 
) 
] 
public 
double 
price 
{ 
get !
;! "
set# &
;& '
}( )
[ 	
Column	 
( 
$str 
) 
] 
public 
double 
amount 
{ 
get "
;" #
set$ '
;' (
}) *
[   	
Column  	 
(   
$str   
)   
]   
public!! 
int!! 
portfolioId!! 
{!!  
get!!! $
;!!$ %
set!!& )
;!!) *
}!!+ ,
}"" 
}## ¥+
q/Users/artemivliev/Artem/Cryptrade/CryptradeBack/TransactionManagement/Migrations/20231129123404_InitialCreate.cs
	namespace 	!
TransactionManagement
 
.  

Migrations  *
{ 
public

 

partial

 
class

 
InitialCreate

 &
:

' (
	Migration

) 2
{ 
	protected 
override 
void 
Up  "
(" #
MigrationBuilder# 3
migrationBuilder4 D
)D E
{ 	
migrationBuilder 
. 
AlterDatabase *
(* +
)+ ,
. 

Annotation 
( 
$str +
,+ ,
$str- 6
)6 7
;7 8
migrationBuilder 
. 
CreateTable (
(( )
name 
: 
$str $
,$ %
columns 
: 
table 
=> !
new" %
{ 
id 
= 
table 
. 
Column %
<% &
int& )
>) *
(* +
type+ /
:/ 0
$str1 6
,6 7
nullable8 @
:@ A
falseB G
)G H
. 

Annotation #
(# $
$str$ C
,C D(
MySqlValueGenerationStrategyE a
.a b
IdentityColumnb p
)p q
,q r
	createdAt 
= 
table  %
.% &
Column& ,
<, -
DateTime- 5
>5 6
(6 7
type7 ;
:; <
$str= J
,J K
nullableL T
:T U
falseV [
)[ \
,\ ]

cryptoName 
=  
table! &
.& '
Column' -
<- .
string. 4
>4 5
(5 6
type6 :
:: ;
$str< F
,F G
nullableH P
:P Q
falseR W
)W X
. 

Annotation #
(# $
$str$ 3
,3 4
$str5 >
)> ?
,? @
cryptoSymbol  
=! "
table# (
.( )
Column) /
</ 0
string0 6
>6 7
(7 8
type8 <
:< =
$str> H
,H I
nullableJ R
:R S
falseT Y
)Y Z
. 

Annotation #
(# $
$str$ 3
,3 4
$str5 >
)> ?
,? @
type 
= 
table  
.  !
Column! '
<' (
string( .
>. /
(/ 0
type0 4
:4 5
$str6 @
,@ A
nullableB J
:J K
falseL Q
)Q R
. 

Annotation #
(# $
$str$ 3
,3 4
$str5 >
)> ?
,? @
buyPrice 
= 
table $
.$ %
Column% +
<+ ,
double, 2
>2 3
(3 4
type4 8
:8 9
$str: B
,B C
nullableD L
:L M
falseN S
)S T
,T U
	buyAmount   
=   
table    %
.  % &
Column  & ,
<  , -
double  - 3
>  3 4
(  4 5
type  5 9
:  9 :
$str  ; C
,  C D
nullable  E M
:  M N
false  O T
)  T U
,  U V
	sellPrice!! 
=!! 
table!!  %
.!!% &
Column!!& ,
<!!, -
double!!- 3
>!!3 4
(!!4 5
type!!5 9
:!!9 :
$str!!; C
,!!C D
nullable!!E M
:!!M N
false!!O T
)!!T U
,!!U V

sellAmount"" 
=""  
table""! &
.""& '
Column""' -
<""- .
double"". 4
>""4 5
(""5 6
type""6 :
:"": ;
$str""< D
,""D E
nullable""F N
:""N O
false""P U
)""U V
,""V W
portfolioId## 
=##  !
table##" '
.##' (
Column##( .
<##. /
int##/ 2
>##2 3
(##3 4
type##4 8
:##8 9
$str##: ?
,##? @
nullable##A I
:##I J
false##K P
)##P Q
}$$ 
,$$ 
constraints%% 
:%% 
table%% "
=>%%# %
{&& 
table'' 
.'' 

PrimaryKey'' $
(''$ %
$str''% 6
,''6 7
x''8 9
=>'': <
x''= >
.''> ?
id''? A
)''A B
;''B C
}(( 
)(( 
.)) 

Annotation)) 
()) 
$str)) +
,))+ ,
$str))- 6
)))6 7
;))7 8
}** 	
	protected-- 
override-- 
void-- 
Down--  $
(--$ %
MigrationBuilder--% 5
migrationBuilder--6 F
)--F G
{.. 	
migrationBuilder// 
.// 
	DropTable// &
(//& '
name00 
:00 
$str00 $
)00$ %
;00% &
}11 	
}22 
}33 Ô
_/Users/artemivliev/Artem/Cryptrade/CryptradeBack/TransactionManagement/Models/Cryptocurrency.cs
	namespace 	!
TransactionManagement
 
.  
Models  &
{ 
public 
class 
Cryptocurrency 
{ 
public 
int 
id 
{ 
get 
; 
set  
;  !
}" #
public 
string 
name 
{ 
get  
;  !
set" %
;% &
}' (
public		 
string		 
symbol		 
{		 
get		 "
;		" #
set		$ '
;		' (
}		) *
public 
double 
price 
{ 
get !
;! "
set# &
;& '
}( )
public 
double 
	marketCap 
{  !
get" %
;% &
set' *
;* +
}, -
public 
double 
	volume24h 
{  !
get" %
;% &
set' *
;* +
}, -
public 
double 
percentChange24h &
{' (
get) ,
;, -
set. 1
;1 2
}3 4
public 
double 
? 
circulatingSupply (
{) *
get+ .
;. /
set0 3
;3 4
}5 6
public 
int 
? 
cmcRank 
{ 
get !
;! "
set# &
;& '
}( )
public 
DateTime 
lastUpdated #
{$ %
get& )
;) *
set+ .
;. /
}0 1
} 
} ®
e/Users/artemivliev/Artem/Cryptrade/CryptradeBack/TransactionManagement/Models/TransactionDataModel.cs
	namespace 	!
TransactionManagement
 
.  
Models  &
{ 
public 
class  
TransactionDataModel "
{ 
public		 
DateTime		 
	createdAt		 !
{		" #
get		$ '
;		' (
set		) ,
;		, -
}		. /
public 
double 
price 
{ 
get !
;! "
set# &
;& '
}( )
public 
double 
amount 
{ 
get "
;" #
set$ '
;' (
}) *
public 
int 
id 
{ 
get 
; 
set  
;  !
}" #
} 
} Ë.
Q/Users/artemivliev/Artem/Cryptrade/CryptradeBack/TransactionManagement/Program.cs
var 
builder 
= 
WebApplication 
. 
CreateBuilder *
(* +
args+ /
)/ 0
;0 1
builder		 
.		 
Services		 
.		 
AddControllers		 
(		  
)		  !
;		! "
builder

 
.

 
Services

 
.

 
	AddScoped

 
<

 
RabbitMQConsumer

 +
>

+ ,
(

, -
)

- .
;

. /
builder 
. 
Services 
. 
	AddScoped 
< 
RabbitMQPublisher ,
>, -
(- .
). /
;/ 0
builder 
. 
Services 
. 
	AddScoped 
< 
TransactionService -
>- .
(. /
)/ 0
;0 1
builder 
. 
Services 
. 
	AddScoped 
< !
CryptocurrencyService 0
>0 1
(1 2
)2 3
;3 4
builder 
. 
Services 
. 
AddHttpClient 
( 
)  
;  !
builder 
. 
Services 
. 
AddControllers 
(  
)  !
;! "
builder 
. 
Services 
. "
AddHttpContextAccessor '
(' (
)( )
;) *
var 
dbHost 

= 
Environment 
. "
GetEnvironmentVariable /
(/ 0
$str0 9
)9 :
;: ;
var 
dbName 

= 
Environment 
. "
GetEnvironmentVariable /
(/ 0
$str0 9
)9 :
;: ;
var 

dbPassword 
= 
Environment 
. "
GetEnvironmentVariable 3
(3 4
$str4 F
)F G
;G H
var 
connectionString 
= 
$" 
$str  
{  !
dbHost! '
}' (
$str( <
{< =
dbName= C
}C D
$strD X
{X Y

dbPasswordY c
}c d
"d e
;e f
builder 
. 
Services 
. 
AddDbContext 
<  
TransactionDbContext 2
>2 3
(3 4
options4 ;
=>< >
options 
. 
UseMySql 
( 
connectionString %
,% &
new' *
MySqlServerVersion+ =
(= >
new> A
VersionB I
(I J
$numJ K
,K L
$numM N
,N O
$numP Q
)Q R
)R S
)S T
)T U
;U V
builder 
. 
Services 
. #
AddEndpointsApiExplorer (
(( )
)) *
;* +
builder 
. 
Services 
. 
AddSwaggerGen 
( 
)  
;  !
builder   
.   
Services   
.   
AddCors   
(   
options    
=>  ! #
{!! 
options"" 
."" 
	AddPolicy"" 
("" 
$str"" *
,""* +
builder## 
=>## 
builder## 
.$$ 
WithOrigins$$ 
($$ 
$str$$ 0
)$$0 1
.%% 
AllowAnyHeader%% 
(%% 
)%% 
.&& 
AllowAnyMethod&& 
(&& 
)&& 
)&& 
;&& 
}'' 
)'' 
;'' 
builder)) 
.)) 
Services)) 
.)) 
AddCors)) 
()) 
options))  
=>))! #
{** 
options++ 
.++ 
	AddPolicy++ 
(++ 
$str++ *
,++* +
builder,, 
=>,, 
builder,, 
.-- 
WithOrigins-- 
(-- 
$str-- 0
)--0 1
... 
AllowAnyHeader.. 
(.. 
).. 
.// 
AllowAnyMethod// 
(// 
)// 
)// 
;// 
}00 
)00 
;00 
var22 
app22 
=22 	
builder22
 
.22 
Build22 
(22 
)22 
;22 
app55 
.55 
UseCors55 
(55 
$str55  
)55  !
;55! "
app66 
.66 
UseCors66 
(66 
$str66  
)66  !
;66! "
if99 
(99 
app99 
.99 
Environment99 
.99 
IsDevelopment99 !
(99! "
)99" #
)99# $
{:: 
app;; 
.;; 

UseSwagger;; 
(;; 
);; 
;;; 
app<< 
.<< 
UseSwaggerUI<< 
(<< 
)<< 
;<< 
}== 
app?? 
.?? 
UseHttpsRedirection?? 
(?? 
)?? 
;?? 
appAA 
.AA 
UseAuthenticationAA 
(AA 
)AA 
;AA 
appCC 
.CC 
UseAuthorizationCC 
(CC 
)CC 
;CC 
appEE 
.EE 
MapControllersEE 
(EE 
)EE 
;EE 
appGG 
.GG 
RunGG 
(GG 
)GG 	
;GG	 
◊
h/Users/artemivliev/Artem/Cryptrade/CryptradeBack/TransactionManagement/Services/CryptocurrencyService.cs
	namespace 	!
TransactionManagement
 
.  
Services  (
{ 
public 

class !
CryptocurrencyService &
{ 
private 
readonly 
RabbitMQConsumer )
_rabbitMQConsumer* ;
;; <
public

 !
CryptocurrencyService

 $
(

$ %
RabbitMQConsumer

& 6
rabbitMQConsumer

7 G
)

G H
{ 	
_rabbitMQConsumer 
= 
rabbitMQConsumer  0
;0 1
} 	
public 
async 
Task 
< 
Cryptocurrency (
>( )1
%GetCryptocurrenciesUsedInTransactions* O
(O P
stringP V

cryptoNameW a
)a b
{ 	
try 
{ 
_rabbitMQConsumer !
.! "
StartConsuming" 0
(0 1
)1 2
;2 3
var 
cryptocurrencies $
=% &
await' ,
_rabbitMQConsumer- >
.> ?*
GetCachedCryptocurrenciesAsync? ]
(] ^

cryptoName^ h
)h i
;i j
return 
cryptocurrencies '
;' (
} 
catch 
( 
	Exception 
ex 
)  
{ 
throw 
ex 
; 
} 
} 	
} 
} –?
c/Users/artemivliev/Artem/Cryptrade/CryptradeBack/TransactionManagement/Services/RabbitMQConsumer.cs
	namespace 	!
TransactionManagement
 
.  
Services  (
{ 
public		 

class		 
RabbitMQConsumer		 !
:		" #
IDisposable		$ /
{

 
private 
readonly 
IConfiguration '
_configuration( 6
;6 7
private 
IConnection 
_connection '
;' (
private 
IModel 
_channel 
;  
private 
List 
< 
Cryptocurrency #
># $#
_cachedCryptocurrencies% <
== >
new? B
ListC G
<G H
CryptocurrencyH V
>V W
(W X
)X Y
;Y Z
private 
readonly 
object 
_lock  %
=& '
new( +
object, 2
(2 3
)3 4
;4 5
private 
bool 
_isConsuming !
=" #
false$ )
;) *
private  
TaskCompletionSource $
<$ %
bool% )
>) *!
_consumptionCompleted+ @
=A B
newC F 
TaskCompletionSourceG [
<[ \
bool\ `
>` a
(a b
)b c
;c d
public 
RabbitMQConsumer 
(  
IConfiguration  .
configuration/ <
)< =
{ 	
_configuration 
= 
configuration *
;* +
InitializeRabbitMQ 
( 
)  
;  !
} 	
private 
void 
InitializeRabbitMQ '
(' (
)( )
{ 	
var 
factory 
= 
new 
ConnectionFactory /
{ 
HostName 
= 
_configuration )
[) *
$str* =
]= >
,> ?
UserName 
= 
_configuration )
[) *
$str* =
]= >
,> ?
Password   
=   
_configuration   )
[  ) *
$str  * =
]  = >
}!! 
;!! 
_connection## 
=## 
factory## !
.##! "
CreateConnection##" 2
(##2 3
)##3 4
;##4 5
_channel$$ 
=$$ 
_connection$$ "
.$$" #
CreateModel$$# .
($$. /
)$$/ 0
;$$0 1
_channel&& 
.&& 
ExchangeDeclare&& $
(&&$ %
_configuration&&% 3
[&&3 4
$str&&4 K
]&&K L
,&&L M
ExchangeType&&N Z
.&&Z [
Fanout&&[ a
)&&a b
;&&b c
_channel'' 
.'' 
QueueDeclare'' !
(''! "
_configuration''" 0
[''0 1
$str''1 E
]''E F
,''F G
durable''H O
:''O P
true''Q U
,''U V
	exclusive''W `
:''` a
false''b g
,''g h

autoDelete''i s
:''s t
false''u z
,''z {
	arguments	''| Ö
:
''Ö Ü
new
''á ä

Dictionary
''ã ï
<
''ï ñ
string
''ñ ú
,
''ú ù
object
''û §
>
''§ •
{
''¶ ß
{
''® ©
$str
''™ ∏
,
''∏ π
$num
''∫ ª
}
''º Ω
}
''æ ø
)
''ø ¿
;
''¿ ¡
_channel(( 
.(( 
	QueueBind(( 
((( 
_configuration(( -
[((- .
$str((. B
]((B C
,((C D
_configuration((E S
[((S T
$str((T k
]((k l
,((l m
$str((n p
)((p q
;((q r
})) 	
public++ 
void++ 
StartConsuming++ "
(++" #
)++# $
{,, 	
if-- 
(-- 
_isConsuming-- 
)-- 
{.. 
return// 
;// 
}00 
var22 
consumer22 
=22 
new22 !
EventingBasicConsumer22 4
(224 5
_channel225 =
)22= >
;22> ?
consumer33 
.33 
Received33 
+=33  
(33! "
model33" '
,33' (
ea33) +
)33+ ,
=>33- /
{44 
try55 
{66 
var77 
body77 
=77 
ea77 !
.77! "
Body77" &
.77& '
ToArray77' .
(77. /
)77/ 0
;770 1
var88 
message88 
=88  !
Encoding88" *
.88* +
UTF888+ /
.88/ 0
	GetString880 9
(889 :
body88: >
)88> ?
;88? @
var;; 
cryptocurrencies;; (
=;;) *
JsonConvert;;+ 6
.;;6 7
DeserializeObject;;7 H
<;;H I
List;;I M
<;;M N
Cryptocurrency;;N \
>;;\ ]
>;;] ^
(;;^ _
message;;_ f
);;f g
;;;g h
lock>> 
(>> 
_lock>> 
)>>  
{?? #
_cachedCryptocurrencies@@ /
=@@0 1
cryptocurrencies@@2 B
;@@B C
}AA !
_consumptionCompletedFF )
.FF) *
TrySetResultFF* 6
(FF6 7
trueFF7 ;
)FF; <
;FF< =
}GG 
catchHH 
(HH 
	ExceptionHH  
exHH! #
)HH# $
{II 
ConsoleKK 
.KK 
	WriteLineKK %
(KK% &
$"KK& (
$strKK( G
{KKG H
exKKH J
}KKJ K
"KKK L
)KKL M
;KKM N
}LL 
}MM 
;MM 
_channelOO 
.OO 
BasicConsumeOO !
(OO! "
queueOO" '
:OO' (
_configurationOO) 7
[OO7 8
$strOO8 L
]OOL M
,OOM N
autoAckOOO V
:OOV W
falseOOX ]
,OO] ^
consumerOO_ g
:OOg h
consumerOOi q
)OOq r
;OOr s
_isConsumingPP 
=PP 
truePP 
;PP  
}QQ 	
publicSS 
asyncSS 
TaskSS 
<SS 
CryptocurrencySS (
>SS( )*
GetCachedCryptocurrenciesAsyncSS* H
(SSH I
stringSSI O

cryptoNameSSP Z
)SSZ [
{TT 	
awaitVV !
_consumptionCompletedVV '
.VV' (
TaskVV( ,
;VV, -
lockXX 
(XX 
_lockXX 
)XX 
{YY 
varZZ 
listZZ 
=ZZ 
newZZ 
ListZZ #
<ZZ# $
CryptocurrencyZZ$ 2
>ZZ2 3
(ZZ3 4#
_cachedCryptocurrenciesZZ4 K
)ZZK L
;ZZL M
Cryptocurrency[[ 
requestedCrypto[[ .
=[[/ 0
null[[1 5
;[[5 6
foreach\\ 
(\\ 
Cryptocurrency\\ '
item\\( ,
in\\- /
list\\0 4
)\\4 5
{]] 
if^^ 
(^^ 
item^^ 
.^^ 
name^^ !
==^^" $

cryptoName^^% /
)^^/ 0
{__ 
requestedCrypto`` '
=``( )
item``* .
;``. /
}aa 
}bb 
returncc 
requestedCryptocc &
;cc& '
}dd 
}ee 	
publicgg 
voidgg 
Disposegg 
(gg 
)gg 
{hh 	
_channelii 
.ii 
Disposeii 
(ii 
)ii 
;ii 
_connectionjj 
.jj 
Disposejj 
(jj  
)jj  !
;jj! "
}kk 	
}ll 
}mm Ω%
d/Users/artemivliev/Artem/Cryptrade/CryptradeBack/TransactionManagement/Services/RabbitMQPublisher.cs
	namespace 	!
TransactionManagement
 
.  
Services  (
{ 
public 

class 
RabbitMQPublisher "
{		 
private

 
readonly

 
IConfiguration

 '
_configuration

( 6
;

6 7
private 
IConnection 
_connection '
;' (
private 
IModel 
_channel 
;  
public 
RabbitMQPublisher  
(  !
IConfiguration! /
configuration0 =
)= >
{ 	
_configuration 
= 
configuration *
;* +
InitializeRabbitMQ 
( 
)  
;  !
} 	
private 
void 
InitializeRabbitMQ '
(' (
)( )
{ 	
var 
factory 
= 
new 
ConnectionFactory /
{ 
HostName 
= 
_configuration )
[) *
$str* D
]D E
,E F
UserName 
= 
_configuration )
[) *
$str* D
]D E
,E F
Password 
= 
_configuration )
[) *
$str* D
]D E
} 
; 
_connection 
= 
factory !
.! "
CreateConnection" 2
(2 3
)3 4
;4 5
_channel 
= 
_connection "
." #
CreateModel# .
(. /
)/ 0
;0 1
_channel!! 
.!! 
ExchangeDeclare!! $
(!!$ %
_configuration!!% 3
[!!3 4
$str!!4 R
]!!R S
,!!S T
ExchangeType!!U a
.!!a b
Fanout!!b h
)!!h i
;!!i j
_channel"" 
."" 
QueueDeclare"" !
(""! "
_configuration""" 0
[""0 1
$str""1 L
]""L M
,""M N
durable""O V
:""V W
true""X \
,""\ ]
	exclusive""^ g
:""g h
false""i n
,""n o

autoDelete""p z
:""z {
false	""| Å
,
""Å Ç
	arguments
""É å
:
""å ç
null
""é í
)
""í ì
;
""ì î
_channel## 
.## 
	QueueBind## 
(## 
_configuration## -
[##- .
$str##. I
]##I J
,##J K
_configuration##L Z
[##Z [
$str##[ y
]##y z
,##z {
$str##| ~
)##~ 
;	## Ä
}$$ 	
public&& 
void&& 
PublishMessage&& "
(&&" #
double&&# )

totalValue&&* 4
,&&4 5
double&&6 <

profitLoss&&= G
,&&G H
int&&I L
portfolioId&&M X
)&&X Y
{'' 	
var(( 
message(( 
=(( 
new(( 
{)) 

totalValue** 
=** 

totalValue** '
,**' (

profitLoss++ 
=++ 

profitLoss++ '
,++' (
portfolioId,, 
=,, 
portfolioId,, )
}-- 
;-- 
var00 
messageJson00 
=00 

Newtonsoft00 (
.00( )
Json00) -
.00- .
JsonConvert00. 9
.009 :
SerializeObject00: I
(00I J
message00J Q
)00Q R
;00R S
var33 
body33 
=33 
Encoding33 
.33  
UTF833  $
.33$ %
GetBytes33% -
(33- .
messageJson33. 9
)339 :
;33: ;
_channel44 
.44 
BasicPublish44 !
(44! "
exchange44" *
:44* +
_configuration44, :
[44: ;
$str44; Y
]44Y Z
,44Z [

routingKey44\ f
:44f g
$str44h j
,44j k
basicProperties44l {
:44{ |
null	44} Å
,
44Å Ç
body
44É á
:
44á à
body
44â ç
)
44ç é
;
44é è
}55 	
public77 
void77 
Dispose77 
(77 
)77 
{88 	
_channel99 
.99 
Dispose99 
(99 
)99 
;99 
_connection:: 
.:: 
Dispose:: 
(::  
)::  !
;::! "
};; 	
}<< 
}== ∏o
e/Users/artemivliev/Artem/Cryptrade/CryptradeBack/TransactionManagement/Services/TransactionService.cs
	namespace 	!
TransactionManagement
 
.  
Services  (
{ 
public		 
class		 
TransactionService		  
{

 
private 
readonly 
RabbitMQPublisher *
_rabbitMQPublisher+ =
;= >
private 
readonly  
TransactionDbContext -
_context. 6
;6 7
private 	
readonly
 !
CryptocurrencyService ("
_cryptocurrencyService) ?
;? @
public 
TransactionService	 
(  
TransactionDbContext 0
context1 8
,8 9!
CryptocurrencyService: O!
cryptocurrencyServiceP e
,e f
RabbitMQPublisherg x
rabbitMQPublisher	y ä
)
ä ã
{ 
_context 
= 
context 
; "
_cryptocurrencyService 
= !
cryptocurrencyService 1
;1 2
_rabbitMQPublisher 
=  
rabbitMQPublisher! 2
;2 3
} 
public 
void 
AddTransaction "
(" #
Transaction# .
model/ 4
)4 5
{ 	
_context 
. 
Transactions 
. 
Add 
( 
model "
)" #
;# $
_context 
. 
SaveChanges 
( 
) 
; 
} 
public 
async	 
Task 
< 
List 
< 
Transaction $
>$ %
>% &(
GetTransactionsByPortfolioId' C
(C D
intD G
portfolioIdH S
)S T
{ 
var 
transactions 
= 
await 
_context $
.$ %
Transactions% 1
.1 2
Where2 7
(7 8
t8 9
=>: <
t= >
.> ?
portfolioId? J
==K M
portfolioIdN Y
)Y Z
.Z [
ToListAsync[ f
(f g
)g h
;h i
if 
( 
transactions 
!= 
null 
) 
{   
return!! 

transactions!! 
;!! 
}"" 
else"" 	
{## 
return$$ 

null$$ 
;$$ 
}%% 
}&& 
public(( 
async(( 
Task(( 
<(( 
List(( 
<(( 
Asset(( $
>(($ %
>((% &"
GetAssetsByPortfolioId((' =
(((= >
int((> A
portfolioId((B M
)((M N
{)) 	
var** 
transactions** 
=** 
await** $
_context**% -
.**- .
Transactions**. :
.**: ;
Where**; @
(**@ A
t**A B
=>**C E
t**F G
.**G H
portfolioId**H S
==**T V
portfolioId**W b
)**b c
.**c d
ToListAsync**d o
(**o p
)**p q
;**q r
List++ 
<++ 
Cryptocurrency++ 
>++  
cryptocurrencies++! 1
=++2 3
new++4 7
List++8 <
<++< =
Cryptocurrency++= K
>++K L
(++L M
)++M N
;++N O
foreach-- 
(-- 
var-- 
transaction-- $
in--% '
transactions--( 4
)--4 5
{.. 
cryptocurrencies//  
.//  !
Add//! $
(//$ %
await//% *"
_cryptocurrencyService//+ A
.//A B1
%GetCryptocurrenciesUsedInTransactions//B g
(//g h
transaction//h s
.//s t

cryptoName//t ~
)//~ 
)	// Ä
;
//Ä Å
}00 
var22 
assets22 
=22 
new22 
List22 !
<22! "
Asset22" '
>22' (
(22( )
)22) *
;22* +
if44 
(44 
transactions44 
!=44 
null44  $
&&44% '
cryptocurrencies44( 8
!=449 ;
null44< @
)44@ A
{55 
foreach66 
(66 
var66 
transaction66 (
in66) +
transactions66, 8
)668 9
{77 
var88 

cryptoInfo88 "
=88# $
cryptocurrencies88% 5
.885 6
FirstOrDefault886 D
(88D E
crypto88E K
=>88L N
crypto88O U
.88U V
name88V Z
==88[ ]
transaction88^ i
.88i j

cryptoName88j t
)88t u
;88u v
if:: 
(:: 

cryptoInfo:: "
!=::# %
null::& *
)::* +
{;; 
var<< 
amount<< "
=<<# $
transaction<<% 0
.<<0 1
amount<<1 7
;<<7 8
var== 
buyPrice== $
===% &
transaction==' 2
.==2 3
price==3 8
;==8 9
var>> 
currentPrice>> (
=>>) *

cryptoInfo>>+ 5
.>>5 6
price>>6 ;
;>>; <
var?? 

profitLoss?? &
=??' (
(??) *
currentPrice??* 6
-??7 8
buyPrice??9 A
)??A B
*??C D
amount??E K
;??K L
var@@ 
percentChange24h@@ ,
=@@- .

cryptoInfo@@/ 9
.@@9 :
percentChange24h@@: J
;@@J K
varBB 
existingAssetBB )
=BB* +
assetsBB, 2
.BB2 3
FirstOrDefaultBB3 A
(BBA B
assetBBB G
=>BBH J
assetBBK P
.BBP Q

cryptoNameBBQ [
==BB\ ^

cryptoInfoBB_ i
.BBi j
nameBBj n
)BBn o
;BBo p
ifDD 
(DD 
existingAssetDD )
!=DD* ,
nullDD- 1
)DD1 2
{EE 
existingAssetGG )
.GG) *
amountGG* 0
+=GG1 3
amountGG4 :
;GG: ;
existingAssetHH )
.HH) *
avgBuyPriceHH* 5
+=HH6 8
buyPriceHH9 A
;HHA B
existingAssetII )
.II) *

profitLossII* 4
+=II5 7

profitLossII8 B
;IIB C
existingAssetJJ )
.JJ) *
numOfTransactionsJJ* ;
+=JJ< >
$numJJ? @
;JJ@ A
}KK 
elseLL 
{MM 
assetsOO "
.OO" #
AddOO# &
(OO& '
newOO' *
AssetOO+ 0
{PP 

cryptoNameQQ  *
=QQ+ ,

cryptoInfoQQ- 7
.QQ7 8
nameQQ8 <
,QQ< =
cryptoSymbolRR  ,
=RR- .

cryptoInfoRR/ 9
.RR9 :
symbolRR: @
,RR@ A
percentChange24hSS  0
=SS1 2
percentChange24hSS3 C
,SSC D
amountTT  &
=TT' (
amountTT) /
,TT/ 0
avgBuyPriceUU  +
=UU, -
buyPriceUU. 6
,UU6 7
currentPriceVV  ,
=VV- .
currentPriceVV/ ;
,VV; <

profitLossWW  *
=WW+ ,

profitLossWW- 7
,WW7 8
portfolioIdXX  +
=XX, -
transactionXX. 9
.XX9 :
portfolioIdXX: E
,XXE F
numOfTransactionsYY  1
=YY2 3
$numYY4 5
}ZZ 
)ZZ 
;ZZ 
}[[ 
}\\ 
}]] 
assets`` 
.`` 
ForEach`` 
(`` 
asset`` $
=>``% '
asset``( -
.``- .
avgBuyPrice``. 9
=``: ;
asset``< A
.``A B
avgBuyPrice``B M
/``N O
asset``P U
.``U V
numOfTransactions``V g
)``g h
;``h i
}aa 
elseaa 
{bb 
returncc 
nullcc 
;cc 
}dd 
doubleff 

totalvalueff 
=ff 
$numff  !
;ff! "
doublehh 

profitlosshh 
=hh 
$numhh  !
;hh! "
foreachjj 
(jj 
Assetjj 
assetjj  
injj! #
assetsjj$ *
)jj* +
{kk 

totalvaluell 
+=ll 
assetll #
.ll# $
amountll$ *
*ll+ ,
assetll- 2
.ll2 3
currentPricell3 ?
;ll? @

profitlossmm 
+=mm 
assetmm #
.mm# $

profitLossmm$ .
;mm. /
}nn 
_rabbitMQPublisherpp 
.pp 
PublishMessagepp -
(pp- .

totalvaluepp. 8
,pp8 9

profitlosspp: D
,ppD E
portfolioIdppF Q
)ppQ R
;ppR S
returnrr 
assetsrr 
;rr 
}ss 	
publicuu 
asyncuu 
Taskuu 
<uu 
Listuu 
<uu 
Transactionuu *
>uu* +
>uu+ ,"
GetTransactionsByAssetuu- C
(uuC D
intuuD G
portfolioIduuH S
,uuS T
stringuuU [
	assetNameuu\ e
)uue f
{vv 	
varxx 
transactionsxx 
=xx 
awaitxx $
_contextxx% -
.xx- .
Transactionsxx. :
.xx: ;
Wherexx; @
(xx@ A
txxA B
=>xxC E
txxF G
.xxG H
portfolioIdxxH S
==xxT V
portfolioIdxxW b
&&xxc e
txxf g
.xxg h

cryptoNamexxh r
==xxs u
	assetNamexxv 
)	xx Ä
.
xxÄ Å
ToListAsync
xxÅ å
(
xxå ç
)
xxç é
;
xxé è
ifyy 
(yy 
transactionsyy 
!=yy 
nullyy  $
)yy$ %
{zz 
return{{ 
transactions{{ #
;{{# $
}|| 
else}} 
{~~ 
return 
null 
; 
}
ÄÄ 
}
ÅÅ 	
public
ÉÉ 
void
ÉÉ 
UpdateTransaction
ÉÉ %
(
ÉÉ% &"
TransactionDataModel
ÉÉ& :
model
ÉÉ; @
,
ÉÉ@ A
int
ÉÉB E
transactionid
ÉÉF S
)
ÉÉS T
{
ÑÑ 
var
ÖÖ 
transaction
ÖÖ 
=
ÖÖ 
_context
ÖÖ &
.
ÖÖ& '
Transactions
ÖÖ' 3
.
ÖÖ3 4
Find
ÖÖ4 8
(
ÖÖ8 9
transactionid
ÖÖ9 F
)
ÖÖF G
;
ÖÖG H
if
áá 
(
áá 
transaction
áá 
!=
áá 
null
áá #
)
áá# $
{
àà 
transaction
ââ 
.
ââ 
	createdAt
ââ 
=
ââ 
model
ââ !
.
ââ! "
	createdAt
ââ" +
;
ââ+ ,
transaction
ää 
.
ää 
amount
ää 
=
ää 
model
ää 
.
ää 
amount
ää %
;
ää% &
transaction
ãã 
.
ãã 
price
ãã 
=
ãã 
model
ãã 
.
ãã 
price
ãã #
;
ãã# $
_context
åå 
.
åå 
SaveChanges
åå 
(
åå 
)
åå 
;
åå 
}
çç 
}
éé 	
public
êê 
void
êê 
DeleteTransaction
êê %
(
êê% &
int
êê& )
id
êê* ,
)
êê, -
{
ëë 	
var
íí 
transaction
íí 
=
íí 
_context
íí 
.
íí 
Transactions
íí *
.
íí* +
FirstOrDefault
íí+ 9
(
íí9 :
t
íí: ;
=>
íí< >
t
íí? @
.
íí@ A
id
ííA C
==
ííD F
id
ííG I
)
ííI J
;
ííJ K
if
ìì 
(
ìì 
transaction
ìì 
!=
ìì 
null
ìì #
)
ìì# $
{
îî 
_context
ïï 
.
ïï 
Transactions
ïï %
.
ïï% &
Remove
ïï& ,
(
ïï, -
transaction
ïï- 8
)
ïï8 9
;
ïï9 :
_context
ññ 
.
ññ 
SaveChanges
ññ $
(
ññ$ %
)
ññ% &
;
ññ& '
}
óó 
else
óó 
{
òò 
throw
ôô 	
new
ôô
 
	Exception
ôô 
(
ôô 
$str
ôô /
)
ôô/ 0
;
ôô0 1
}
öö 
}
õõ 	
public
ùù 
void
ùù -
DeleteTransactionsByPortfolioId
ùù 3
(
ùù3 4
int
ùù4 7
portfolioId
ùù8 C
)
ùùC D
{
ûû 	
var
üü 
transactions
üü 
=
üü 
_context
üü '
.
üü' (
Transactions
üü( 4
.
üü4 5
Where
üü5 :
(
üü: ;
t
üü; <
=>
üü= ?
t
üü@ A
.
üüA B
portfolioId
üüB M
==
üüN P
portfolioId
üüQ \
)
üü\ ]
.
üü] ^
ToList
üü^ d
(
üüd e
)
üüe f
;
üüf g
if
°° 
(
°° 
transactions
°° 
.
°° 
Any
°°  
(
°°  !
)
°°! "
)
°°" #
{
¢¢ 
_context
££ 
.
££ 
Transactions
££ %
.
££% &
RemoveRange
££& 1
(
££1 2
transactions
££2 >
)
££> ?
;
££? @
_context
§§ 
.
§§ 
SaveChanges
§§ $
(
§§$ %
)
§§% &
;
§§& '
}
•• 
else
¶¶ 
{
ßß 
throw
®® 
new
®® 
	Exception
®® #
(
®®# $
$str
®®$ V
)
®®V W
;
®®W X
}
©© 
}
™™ 	
}
´´ 
}¨¨ 