#ifndef MINEFIELD_H
#define MINEFIELD_H
#include <vector>
#include <string>
#include <sstream>
#include <algorithm>
#include <iostream>
#include <exception>
#include <stdexcept>
#include "Typedefs.h"
#define MINE -1

class MineField
{
	friend inline std::ostream& operator<<(std::ostream& out, const MineField& mineField)
	{
		out << "Field#" << mineField.m_fieldNum << std::endl;
		for (auto& i : mineField.m_fieldData)
		{
			for (auto& by : i)
			{
				if (by == MINE)
				{
					out << "*";
				}
				else
				{
					out << (int)by;
				}
			}
			out << "\n";
		}
		return out;
	}
public:
	MineField(const mineFieldData fieldData, const int fieldNum) :m_fieldData(fieldData), m_fieldNum(fieldNum) {};
	virtual ~MineField();
	//returns a vector of all the Minefields
	static std::vector<MineField> getMineFields(const std::vector<std::string>& stringVec);
private:
	mineFieldData m_fieldData;
	int m_fieldNum;

	//returns the mineFieldData of one mine from the given string data
	static mineFieldData getMineData(const std::vector<std::string>& stringData);
	//returns the minefield data from the the given string but grouped so each outer vector contains the contents of one minefield of data
	static std::vector<std::vector<std::string>> getStringData(const std::vector<std::string>& stringVec);
	//updates a given field so each square has a count of number of mines neighboring
	static void updateMineField(mineFieldData& fieldData,const std::vector<point>& minePos);
	//updates a given field and updates based on one mine
	static void updateMineFieldHelper(mineFieldData& fieldData, const point minePos);
	static inline bool isMine(const mineFieldData& fieldData, const point pos)noexcept 
	{ 
		if (fieldData[pos.first][pos.second] == MINE)
		{ 
			return true; 
		} 
		return false;
	}
};

#endif
