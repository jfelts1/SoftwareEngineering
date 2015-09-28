/*
James Felts 2015
*/
#ifndef FILEUTILS_H
#define FILEUTILS_H
#include <string>
#include <iostream>
#include <fstream>
namespace Utils
{
	//returns the entire file as a string
	std::string readFileAsText(std::string filepath);
}
#endif