#property strict

#include <Files/File.mqh>


class CMyFilePipe : public CFile
{
public:
   int msTimeout;
   int sleep;
                     CMyFilePipe(int _msTimeout,int _sleep);
                    ~CMyFilePipe(void);
   int               Open(const string file_name,const int open_flags);
   bool              WaitForRead(const ulong size);
   template<typename T>
   uint              WriteInteger(const T value);
   uint              WriteLong(const long value);
   uint              WriteFloat(const float value);
   uint              WriteDouble(const double value);
   uint              WriteString(const string value);
   uint              WriteString(const string value,const int size);
   template<typename T>
   uint              WriteArray(T &array[],const int start_item=0,const int items_count=WHOLE_ARRAY);
   template<typename T>
   uint              WriteStruct(T &data);
   bool              WriteObject(CObject *object);
   template<typename T>
   uint              WriteEnum(const T value) { return(WriteInteger((int)value)); }
   template<typename T>
   bool              ReadInteger(T &value);
   bool              ReadLong(long &value);
   bool              ReadFloat(float &value);
   bool              ReadDouble(double &value);
   bool              ReadString(string &value);
   bool              ReadString(string &value,const int size);
   template<typename T>
   uint              ReadArray(T &array[],const int start_item=0,const int items_count=WHOLE_ARRAY);
   template<typename T>
   uint              ReadStruct(T &data);
   bool              ReadObject(CObject *object);
   template<typename T>
   bool              ReadEnum(T &value);
};
CMyFilePipe::CMyFilePipe(int _msTimeout,int _sleep)
{
   msTimeout=_msTimeout;
   sleep=_sleep;
}
CMyFilePipe::~CMyFilePipe(void)
{
}
int CMyFilePipe::Open(const string file_name,const int open_flags)
{
   return(CFile::Open(file_name,open_flags|FILE_BIN));
}
bool CMyFilePipe::WaitForRead(const ulong size)
{
   int counter=msTimeout;
   while(m_handle!=INVALID_HANDLE && !IsStopped() && counter>0)
   {
      if(FileSize(m_handle)>=size)
         return(true);
      Sleep(sleep);
      counter-=sleep;
   }
   return(false);
}
template<typename T>
uint CMyFilePipe::WriteInteger(const T value)
{
   if(m_handle!=INVALID_HANDLE)
      return(FileWriteInteger(m_handle,value,sizeof(T)));
   return(0);
}
uint CMyFilePipe::WriteLong(const long value)
{
   if(m_handle!=INVALID_HANDLE)
      return(FileWriteLong(m_handle,value));
   return(0);
}
uint CMyFilePipe::WriteFloat(const float value)
{
   if(m_handle!=INVALID_HANDLE)
      return(FileWriteFloat(m_handle,value));
   return(0);
}
uint CMyFilePipe::WriteDouble(const double value)
{
   if(m_handle!=INVALID_HANDLE)
      return(FileWriteDouble(m_handle,value));
   return(0);
}
uint CMyFilePipe::WriteString(const string value)
{
   if(m_handle!=INVALID_HANDLE)
   {
      int size=StringLen(value);
      if(FileWriteInteger(m_handle,size)==sizeof(int))
         return(FileWriteString(m_handle,value,size));
   }
   return(0);
}
uint CMyFilePipe::WriteString(const string value,const int size)
{
   if(m_handle!=INVALID_HANDLE)
      return(FileWriteString(m_handle,value,size));
   return(0);
}
template<typename T>
uint CMyFilePipe::WriteArray(T &array[],const int start_item=0,const int items_count=WHOLE_ARRAY)
{
   if(m_handle!=INVALID_HANDLE)
      return(FileWriteArray(m_handle,array,start_item,items_count));
   return(0);
}
template<typename T>
uint CMyFilePipe::WriteStruct(T &data)
{
   if(m_handle!=INVALID_HANDLE)
      return(FileWriteStruct(m_handle,data));
   return(0);
}
bool CMyFilePipe::WriteObject(CObject *object)
{
   if(m_handle!=INVALID_HANDLE)
      if(CheckPointer(object))
         return(object.Save(m_handle));
   return(false);
}
template<typename T>
bool CMyFilePipe::ReadInteger(T &value)
{
   if(WaitForRead(sizeof(T)))
   {
      ResetLastError();
      value=FileReadInteger(m_handle,sizeof(T));
      return(GetLastError()==0);
   }
   return(false);
}
bool CMyFilePipe::ReadLong(long &value)
{
   if(WaitForRead(sizeof(long)))
   {
      ResetLastError();
      value=FileReadLong(m_handle);
      return(GetLastError()==0);
   }
   return(false);
}
bool CMyFilePipe::ReadFloat(float &value)
{
   if(WaitForRead(sizeof(float)))
   {
      ResetLastError();
      value=FileReadFloat(m_handle);
      return(GetLastError()==0);
   }
   return(false);
}
bool CMyFilePipe::ReadDouble(double &value)
{
   if(WaitForRead(sizeof(double)))
   {
      ResetLastError();
      value=FileReadDouble(m_handle);
      return(GetLastError()==0);
   }
   return(false);
}
bool CMyFilePipe::ReadString(string &value)
{
   if(WaitForRead(sizeof(int)))
   {
      ResetLastError();
      int size=FileReadInteger(m_handle);
      if(GetLastError()==0)
      {
         if(WaitForRead(size))
         {
            value=FileReadString(m_handle,size);
            return(size==StringLen(value));
         }
      }
   }
   return(false);
}
bool CMyFilePipe::ReadString(string &value,const int size)
{
   if(WaitForRead(size))
   {
      value=FileReadString(m_handle,size);
      return(size==StringLen(value));
   }
   return(false);
}
template<typename T>
uint CMyFilePipe::ReadArray(T &array[],const int start_item=0,const int items_count=WHOLE_ARRAY)
{
   uint size=ArraySize(array);
   if(items_count!=WHOLE_ARRAY) size=items_count;
   if(WaitForRead(size*sizeof(T)))
      return(FileReadArray(m_handle,array,start_item,items_count));
   return(0);
}
template<typename T>
uint CMyFilePipe::ReadStruct(T &data)
{
   if(WaitForRead(sizeof(T)))
      return(FileReadStruct(m_handle,data));
   return(0);
}
bool CMyFilePipe::ReadObject(CObject *object)
{
   if(CheckPointer(object))
      if(WaitForRead(sizeof(int))) // only 4 bytes!
         return(object.Load(m_handle));
   return(false);
}
template<typename T>
bool CMyFilePipe::ReadEnum(T &value)
{
   int val;
   if(!ReadInteger(val))
      return(false);
   value=(T)val;
   return(true);
}


class mtsPipeMessageClient
{
private:
   string pipeName;
public:
   uchar response[];
   mtsPipeMessageClient()
   {
   }
   ~mtsPipeMessageClient()
   {
      ArrayFree(response);
   }
   void initialize(string _channelName)
   {
      pipeName="\\\\.\\pipe\\"+_channelName;
      ArrayResize(response,0);
   }
   bool send(uchar &_request[], int _msTimeout, int _sleep)
   {
      ArrayResize(response,0);

      int timeout=0;
      int sleep=_sleep;
      
      CMyFilePipe pipe(_msTimeout,_sleep);
      while (timeout<_msTimeout)
      {
         if (pipe.Open(pipeName,FILE_READ|FILE_WRITE|FILE_BIN)==INVALID_HANDLE)
         {
            Sleep(sleep);
            timeout+=sleep;
            if (timeout>=_msTimeout) return false;
         }
         else
         {
            break;
         }
      }
      
      int requestSize=ArraySize(_request);
      if (pipe.WriteInteger(requestSize)!=4) return false;
      if (pipe.WriteArray(_request)!=requestSize) return false;
      
      int responseSize=0;
      if (!pipe.ReadInteger(responseSize)) return false;
      if (responseSize>0)
      {
         ArrayResize(response,responseSize);
         if (pipe.ReadArray(response)!=responseSize) return false;
         return true;
      }
      return false;
   }
};
union mtsPackageInt
{
   int value;
   uchar bytes[4];
};
union mtsPackageUInt
{
   uint value;
   uchar bytes[4];
};
union mtsPackageInt64
{
   long value;
   uchar bytes[8];
};
union mtsPackageUInt64
{
   ulong value;
   uchar bytes[8];
};
union mtsPackageDouble
{
   double value;
   uchar bytes[8];
};

class mtsVEncoder
{
public:
   uchar data[];
   mtsVEncoder()
   {
      ArrayResize(data,0);
   }
   ~mtsVEncoder()
   {
      ArrayFree(data);
   }
private:
   void push(uchar &_newData[])
   {
      int oldSize=ArraySize(data);
      ArrayResize(data,oldSize+ArraySize(_newData));
      ArrayCopy(data,_newData,oldSize);
   }
public:
   void addByte(uchar _value)
   {
      int oldSize=ArraySize(data);
      ArrayResize(data,oldSize+1);
      data[oldSize]=_value;
   }
   void addInt(int _value)
   {
      mtsPackageInt package;
      package.value=_value;
      push(package.bytes);
   }
   void addUInt(uint _value)
   {
      mtsPackageUInt package;
      package.value=_value;
      push(package.bytes);
   }
   void addInt64(long _value)
   {
      mtsPackageInt64 package;
      package.value=_value;
      push(package.bytes);
   }
   void addUInt64(ulong _value)
   {
      mtsPackageUInt64 package;
      package.value=_value;
      push(package.bytes);
   }
   void addDouble(double _value)
   {
      mtsPackageDouble package;
      package.value=_value;
      push(package.bytes);
   }
   void addString(string _value)
   {
      uchar _data[];
      StringToCharArray(_value,_data,0,WHOLE_ARRAY,CP_UTF8);
      addArray(_data);
   }
   void addArray(uchar &_newData[])
   {
      addInt(ArraySize(_newData));
      push(_newData);
   }
};

class mtsTrainEncoder
{
public:
   uchar data[];
   mtsTrainEncoder()
   {
      ArrayResize(data,0);
   }
   ~mtsTrainEncoder()
   {
      ArrayFree(data);
   }
   void addChunk(uchar &_chunk[])
   {
      push(ArraySize(_chunk));
      push(_chunk);
   }
private:
   void push(uchar &_newData[])
   {
      int oldSize=ArraySize(data);
      ArrayResize(data,oldSize+ArraySize(_newData));
      ArrayCopy(data,_newData,oldSize);
   }
   void push(int _value)
   {
      mtsPackageInt package;
      package.value=_value;
      push(package.bytes);
   }
};

//////////////////////////////////////////////////////////////////////
class mtsVDecoder
{
private:
   uchar data[];
   int pos;
   int dataSize;
public:
   uchar arrayOutput[];
   mtsVDecoder(uchar &_data[], int _offset=0, int _size=0)
   {
      reset(_data,_offset,_size);
   }
   ~mtsVDecoder()
   {
      ArrayFree(data);
      ArrayFree(arrayOutput);
   }
   void reset(uchar &_data[], int _offset=0, int _size=0)
   {
      dataSize=_size==0 ? ArraySize(_data)-_offset : _size;
      ArrayResize(data,dataSize);
      ArrayCopy(data,_data,0,_offset,dataSize);
      pos=0;
      ArrayResize(arrayOutput,0);
   }
private:
   int pop(int _len)
   {
      int result = pos;
      if ((pos+_len)<=dataSize)
      {
          pos += _len;
      }
      else
      {
          result = -1;
          pos = dataSize;
      }
      return result;
   }
   void copy(uchar &_data[], int _pos, int _len)
   {
      ArrayCopy(_data,data,0,_pos,_len);
   }
public:
   uchar readByte()
   {
      int result = pop(1);
      if (result >=0)
      {
          return data[result];
      }
      return 0;
   }
   int readInt()
   {
      int result = pop(4);
      if (result >= 0)
      {
         mtsPackageInt value;
         copy(value.bytes,result,4);
         return value.value;
      }
      return 0;
   }
   uint readUInt()
   {
      int result = pop(4);
      if (result >= 0)
      {
         mtsPackageUInt value;
         copy(value.bytes,result,4);
         return value.value;
      }
      return 0;
   }
   long readInt64()
   {
      int result = pop(8);
      if (result >= 0)
      {
         mtsPackageInt64 value;
         copy(value.bytes,result,8);
         return value.value;
      }
      return 0;
   }
   ulong readUInt64()
   {
      int result = pop(8);
      if (result >= 0)
      {
         mtsPackageUInt64 value;
         copy(value.bytes,result,8);
         return value.value;
      }
      return 0;
   }
   double readDouble()
   {
      int result = pop(8);
      if (result >= 0)
      {
         mtsPackageDouble value;
         copy(value.bytes,result,8);
         return value.value;
      }
      return 0;
   }
   int readArray()
   {
      ArrayResize(arrayOutput,0);
      int len = readInt();
      if (len > 0)
      {
          int result = pop(len);
          if (result >= 0)
          {
              ArrayResize(arrayOutput,len);
              copy(arrayOutput,result,len);
              return len;
          }
      }
      return 0;   
   }
   string readString()
   {
      int len = readArray();
      if (len>0)
      {
         return CharArrayToString(arrayOutput,0,WHOLE_ARRAY,CP_UTF8);
      }
      return "";
   }
};

class mtsTrainDecoder
{
private:
   uchar data[];
   int pos;
   int dataSize;
public:
   uchar chunkOutput[];
   mtsTrainDecoder(uchar &_data[], int _offset=0, int _size=0)
   {
      reset(_data,_offset,_size);
   }
   ~mtsTrainDecoder()
   {
      ArrayFree(data);
      ArrayFree(chunkOutput);
   }
   void reset(uchar &_data[], int _offset=0, int _size=0)
   {
      dataSize=_size==0 ? ArraySize(_data)-_offset : _size;
      ArrayResize(data,dataSize);
      ArrayCopy(data,_data,0,_offset,dataSize);
      pos=0;
      ArrayResize(chunkOutput,0);
   }
   int next()
   {
      ArrayResize(chunkOutput,0);
      if ((pos + 4) > dataSize)
      {
          pos = 0;
          return 0;
      }
      mtsPackageInt len;
      ArrayCopy(len.bytes,data,0,pos,4);
      pos += 4;

      if ((pos + len.value) > dataSize)
      {
          pos = 0;
          return 0;
      }
      ArrayResize(chunkOutput,len.value);
      ArrayCopy(chunkOutput,data,0,pos,len.value);
      pos += len.value;
      return len.value;
   }
};


