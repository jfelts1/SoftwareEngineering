#ifndef MINEFIELD_H
#define MINEFIELD_H
#include <vector>
#include <string>
#include <sstream>
#include "Typedefs.h"
#define MINE -1

class MineField
{
public:
	MineField(const std::vector<std::vector<byte>> fieldData, const int fieldNum) :m_fieldData(fieldData), m_fieldNum(fieldNum) {};
	virtual ~MineField();
	static std::vector<MineField> getMineFields(const std::vector<std::string> stringVec);
private:
	std::vector<std::vector<byte>> m_fieldData;
	int m_fieldNum;
	static std::vector<byte> getMineData(const std::vector<std::string> stringData);
	static std::vector<std::vector<std::string>> getStringData(const std::vector<std::string> stringVec);
};

#endif
