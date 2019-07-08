// rapidjson/example/simpledom/simpledom.cpp`
#include "include/rapidjson/document.h"
#include "rapidjson/writer.h"
#include "rapidjson/stringbuffer.h"
#include <iostream>
using namespace rapidjson;
int main() {
	// 1. Parse a JSON string into DOM.
	const char* json = "{\"project\":\"rapidjson\",\"stars\":10}";
	Document d;
	d.Parse(json);
	// 2. Modify it by DOM.
	Value& s = d["stars"];
	s.SetInt(s.GetInt() + 1);
	// 3. Stringify the DOM
	StringBuffer buffer;
	Writer<StringBuffer> writer(buffer);
	d.Accept(writer);
	// Output {"project":"rapidjson","stars":11}
	std::cout << buffer.GetString() << std::endl;


	{
		"hello": "world",
			"t" : true,
			"f" : false,
			"n" : null,
			"i" : 123,
			"pi" : 3.1416,
			"a" : [1, 2, 3, 4]
	}



	return 0;


}