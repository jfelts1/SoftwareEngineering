/*
James Felts 2015
*/
#ifndef STRINGUTILS_H
#define STRINGUTILS_H
#include <string>
#include <cstring>
#include <cwchar>
#include <cctype>
#include <iostream>
#include <exception>
#include <stdexcept>
#include <memory>
#include <vector>

typedef wchar_t wchar;

namespace Utils
{
	//returns the substring nested between two other strings
	std::string getStringBetweenTwoStrings(const std::string& str,const std::string& stringOne,const std::string& stringTwo);
	//finds the first time a string appears in another string
	//returns -1 if nothing is found
	int findFirstStringInString(const std::string& str, const std::string& lookFor,const int startAt = 0) noexcept;
	//returns the substring of the passed in string use instead of substr when determining the count parameter of substr is hard
	//throws range_error
	std::string subString(const std::string& str, const int start, const int end);
	//returns a vector of strings from an original string split on the given delimitor
	std::vector<std::string> splitString(const std::string& str, const char delim);

	//returns the passed string with whitespace removed from the begining
	inline std::string ltrim(std::string& str)
	{
		if(str != "")
		{
			int i = 0;
			while (isspace((unsigned char)str.front()))
			{
				str.erase(i, 1);
				i++;
			}
		}
		return str;
	}
	//returns the passed string with whitespace removed from the end
	inline std::string rtrim(std::string& str)
	{
		if(str != "")
		{
			while (isspace((unsigned char)str.back()))
			{
				str.pop_back();
			}
		}
		return str;
	}
	//returns the passed string with whitespace removed from the begining and end
	inline std::string trim(std::string str)
	{
		Utils::ltrim(str);
		Utils::rtrim(str);
		return str;
	}
	
	namespace OddlySpecificUtils
	{
		//removes the number= from the begining of a string
		inline std::string removeNumberEqualsFromBeginingOfString(std::string& textureDef)
		{
			size_t pos = textureDef.find_first_of('=', 0);
			textureDef = textureDef.substr(pos + 1);
			return textureDef;
		}

	}
}

#endif
