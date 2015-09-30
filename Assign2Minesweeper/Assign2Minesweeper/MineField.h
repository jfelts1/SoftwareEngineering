#ifndef MINEFIELD_H
#define MINEFIELD_H
#include <vector>
#include <string>
#include <sstream>
#include <algorithm>
#include "Typedefs.h"
#define MINE -1

class MineField
{
public:
	MineField(const mineFieldData fieldData, const int fieldNum) :m_fieldData(fieldData), m_fieldNum(fieldNum) {};
	virtual ~MineField();
	//returns a vector of all the Minefields
	static std::vector<MineField> getMineFields(const std::vector<std::string>& stringVec);
private:
	mineFieldData m_fieldData;
	int m_fieldNum;
	//returns the mineFieldData from the given string data
	static mineFieldData getMineData(const std::vector<std::string>& stringData);
	static std::vector<std::vector<std::string>> getStringData(const std::vector<std::string>& stringVec);
	static void updateMineField(mineFieldData& fieldData, std::vector<point>& minePos);
};

#endif
