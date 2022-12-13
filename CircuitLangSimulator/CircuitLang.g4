grammar CircuitLang;

program: module*;

module
    : MAIN? MODULE IDENTIFIER '(' module_argument_list ')' '->' module_output_list ':'
        statement*
    ;

module_argument_list: (identifier_with_array (',' identifier_with_array)*)?;

module_output_list: identifier_with_array (',' identifier_with_array)*;

identifier_with_array : IDENTIFIER ('[' DEC_NUMBER ']')?; 

statement: assignment | variable_declaration;

assignment
    : identifier_with_array (',' identifier_with_array)* '=' expression (',' expression)*
    ;

expression : identifier_with_array
		   | literal
		   | '(' expression ')'
		   | expression binary_operator expression
		   | unary_operation
		   | module_call
		   | truth_table;
		   
unary_operation: unary_operator expression;
		   
binary_operator: '&' | '|' | '^';

unary_operator: '~';

module_call: IDENTIFIER '(' (expression (',' expression)*)? ')';

variable_declaration : 'var' identifier_with_array;

truth_table : TRUTH_TABLE '(' expression (',' expression)* ')' '{' truth_table_entry* '}';

truth_table_entry : literal (',' literal)* ':' literal_or_wild (',' literal_or_wild)* NEWLINE;

literal_or_wild : literal | '?';

literal      : DEC_NUMBER | BIN_NUMBER | OCT_NUMBER | HEX_NUMBER;


MODULE       : 'module';
MAIN         : 'main';
TRUTH_TABLE  : 'truth_table';
IDENTIFIER   : [a-zA-Z][a-zA-Z0-9]*;
WS           : [ \t\r\n]+ -> skip;
LINE_COMMENT : '#' ~[\r\n]* -> skip;
NEWLINE      : [\r\n];

// decimal number with optional decimal point and fractional part
DEC_NUMBER   : [0-9]+;

// binary number (starts with 0b)
BIN_NUMBER   : '0b' [01]+;

// octal number (starts with 0o)
OCT_NUMBER   : '0o' [0-7]+;

// hexadecimal number (starts with 0x)
HEX_NUMBER   : '0x' [0-9a-fA-F]+;
