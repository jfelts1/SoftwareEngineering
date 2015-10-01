#include "MineField.h"

using std::vector;
using std::string;
using std::stringstream;

MineField::~MineField()
{}

std::vector<MineField> MineField::getMineFields(const std::vector<std::string>& stringVec)
{
	vector<MineField> ret;
	//vector<vector<byte>> fieldData;
	
	int count = 0;
	std::vector<std::vector<std::string>> stringData = getStringData(stringVec);
	mineFieldData mineData;
	for(auto& mineDataString : stringData)
	{
		mineData = getMineData(mineDataString);
		
		ret.emplace_back(MineField(mineData,count));
		mineData.clear();
		count++;
	}
	

	return ret;
}

mineFieldData MineField::getMineData(const std::vector<std::string>& stringData)
{
	mineFieldData ret;
	vector<point> minePos;
	for(size_t i = 0;i<stringData.size();i++)
	{
		vector<byte> tmp;
		for(size_t j = 0;j<stringData[i].size();j++)
		{
			if(stringData[i][j] == '*')
			{
				tmp.emplace_back(MINE);
				minePos.emplace_back(point(i,j));
			}
			else
			{
				tmp.emplace_back(0);
			}
		}
		ret.emplace_back(tmp);
	}
	updateMineField(ret,minePos);
	return ret;
}

std::vector<std::vector<std::string>> MineField::getStringData(const std::vector<std::string>& stringVec)
{
	vector<vector<string>> ret;
	vector<string> mineDataString;
	int x, y;
	stringstream tmp;
	for (auto& strRow : stringVec)
	{
		x = y = 0;
		tmp = stringstream(strRow);
		tmp >> x;
		tmp >> y;
		
		if((x!=0 && y!=0)||(x!=0 || y!=0))
		{
			ret.emplace_back(mineDataString);
			mineDataString.clear();
		}
		//don't read the lines with numbers
		else if (x == 0 && y == 0)
		{
			mineDataString.emplace_back(strRow);
		}
	}	
	return ret;
}

void MineField::updateMineField(mineFieldData& fieldData,const std::vector<point>& minePos)
{
	for (auto& pos: minePos)
	{
		updateMineFieldHelper(fieldData, pos);
	}
}

void MineField::updateMineFieldHelper(mineFieldData & fieldData, const point minePos)
{
	size_t x = minePos.first;
	size_t y = minePos.second;
#define UPDATE_IF_NOT_MINE(x,y)\
if(fieldData.at(x).at(y)!=MINE)\
{\
	fieldData.at(x).at(y)++;\
}
	try
	{
		if (x > 0 && y > 0)
		{
			UPDATE_IF_NOT_MINE(x - 1, y - 1)
		}
		if (x > 0)
		{
			UPDATE_IF_NOT_MINE(x - 1, y)
		}
		if (x > 0 && y < fieldData[x].size()-1)
		{
			UPDATE_IF_NOT_MINE(x - 1, y + 1)
		}
		if (y > 0)
		{
			UPDATE_IF_NOT_MINE(x , y - 1)
		}
		if (y < fieldData[x].size()-1)
		{
			UPDATE_IF_NOT_MINE(x , y +1)
		}
		if (x < fieldData.size()-1 && y > 0)
		{
			UPDATE_IF_NOT_MINE(x + 1, y-1)
		}
		if (x < fieldData.size()-1)
		{
			UPDATE_IF_NOT_MINE(x + 1, y )
		}
		if (x < fieldData.size()-1 && y < fieldData[x].size()-1)
		{
			UPDATE_IF_NOT_MINE(x + 1, y + 1)
		}
	}
	catch (std::out_of_range& e)
	{
		std::cerr << e.what() << std::endl;
	}

}
