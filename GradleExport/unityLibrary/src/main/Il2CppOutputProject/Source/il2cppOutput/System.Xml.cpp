#include "pch-cpp.hpp"





struct VirtualActionInvoker0
{
	typedef void (*Action)(void*, const RuntimeMethod*);

	static inline void Invoke (Il2CppMethodSlot slot, RuntimeObject* obj)
	{
		const VirtualInvokeData& invokeData = il2cpp_codegen_get_virtual_invoke_data(slot, obj);
		((Action)invokeData.methodPtr)(obj, invokeData.method);
	}
};
template <typename T1>
struct VirtualActionInvoker1
{
	typedef void (*Action)(void*, T1, const RuntimeMethod*);

	static inline void Invoke (Il2CppMethodSlot slot, RuntimeObject* obj, T1 p1)
	{
		const VirtualInvokeData& invokeData = il2cpp_codegen_get_virtual_invoke_data(slot, obj);
		((Action)invokeData.methodPtr)(obj, p1, invokeData.method);
	}
};
template <typename R>
struct VirtualFuncInvoker0
{
	typedef R (*Func)(void*, const RuntimeMethod*);

	static inline R Invoke (Il2CppMethodSlot slot, RuntimeObject* obj)
	{
		const VirtualInvokeData& invokeData = il2cpp_codegen_get_virtual_invoke_data(slot, obj);
		return ((Func)invokeData.methodPtr)(obj, invokeData.method);
	}
};
template <typename R, typename T1>
struct VirtualFuncInvoker1
{
	typedef R (*Func)(void*, T1, const RuntimeMethod*);

	static inline R Invoke (Il2CppMethodSlot slot, RuntimeObject* obj, T1 p1)
	{
		const VirtualInvokeData& invokeData = il2cpp_codegen_get_virtual_invoke_data(slot, obj);
		return ((Func)invokeData.methodPtr)(obj, p1, invokeData.method);
	}
};
template <typename T1>
struct GenericVirtualActionInvoker1
{
	typedef void (*Action)(void*, T1, const RuntimeMethod*);

	static inline void Invoke (const RuntimeMethod* method, RuntimeObject* obj, T1 p1)
	{
		VirtualInvokeData invokeData;
		il2cpp_codegen_get_generic_virtual_invoke_data(method, obj, &invokeData);
		((Action)invokeData.methodPtr)(obj, p1, invokeData.method);
	}
};
template <typename T1>
struct InterfaceActionInvoker1
{
	typedef void (*Action)(void*, T1, const RuntimeMethod*);

	static inline void Invoke (Il2CppMethodSlot slot, RuntimeClass* declaringInterface, RuntimeObject* obj, T1 p1)
	{
		const VirtualInvokeData& invokeData = il2cpp_codegen_get_interface_invoke_data(slot, obj, declaringInterface);
		((Action)invokeData.methodPtr)(obj, p1, invokeData.method);
	}
};
template <typename T1>
struct GenericInterfaceActionInvoker1
{
	typedef void (*Action)(void*, T1, const RuntimeMethod*);

	static inline void Invoke (const RuntimeMethod* method, RuntimeObject* obj, T1 p1)
	{
		VirtualInvokeData invokeData;
		il2cpp_codegen_get_generic_interface_invoke_data(method, obj, &invokeData);
		((Action)invokeData.methodPtr)(obj, p1, invokeData.method);
	}
};

struct ByteU5BU5D_tA6237BF417AE52AD70CFB4EF24A7A82613DF9031;
struct CharU5BU5D_t799905CF001DD5F13F7DBB310181FC4D8B7D0AAB;
struct DelegateU5BU5D_tC5AB7E8F745616680F337909D3A8E6C722CDF771;
struct IntPtrU5BU5D_tFD177F8C806A6921AD7150264CCC62FA00CAD832;
struct ObjectU5BU5D_t8061030B0A12A55D5AD8652A20C922FE99450918;
struct StackTraceU5BU5D_t32FBCB20930EAF5BAE3F450FF75228E5450DA0DF;
struct StringU5BU5D_t7674CD946EC0CE7B3AE0BE70E6EE85F2ECD9F248;
struct ArgumentOutOfRangeException_tEA2822DAF62B10EEED00E0E3A341D4BAF78CF85F;
struct ArrayList_t7A8E5AF0C4378015B5731ABE2BED8F2782FEEF8A;
struct Delegate_t;
struct DelegateData_t9B286B493293CD2D23A5B2B5EF0E5B1324C2B77E;
struct EmptyEnumerator_t84EC9187C8460EE98E675ED9258AE4DF2A6776DA;
struct Enum_t2A1A94B24E3B776EEF4E5E485E290BB9D4D072E2;
struct IDictionary_t6D03155AF1FA9083817AA5B6AD7DEEACC26AB220;
struct IEnumerator_t7B609C2FFA6EB5167D9C62A0C32A21DE2F666DAA;
struct IXmlSchemaInfo_tF7DB6310A471259B33C4081B30E73925164204DB;
struct IndexOutOfRangeException_t7ECB35264FB6CA8FAA516BD958F4B2ADC78E8A82;
struct InvalidOperationException_t5DDE4D49B7405FAAB1E4576F4715A42A3FAD4BAB;
struct MethodInfo_t;
struct SafeSerializationManager_tCBB85B95DFD1634237140CD892E82D06ECB3F5E6;
struct String_t;
struct StringBuilder_t;
struct Void_t4861ACF8F4594C3437BB48B6E56783494B843915;
struct WeakReference_tD4B0518CE911FFD9FAAB3FCD492644A354312D8E;
struct XmlAttribute_t4B6CC461196FBF5CC9F777E74CC82C98E0CA9D18;
struct XmlAttributeCollection_tB0BA25B3C7E8D4BCF487C7107A9019632A7D85C0;
struct XmlChildEnumerator_tDD03B83EAA250AB86DFF4B53C1430DA942E85939;
struct XmlChildNodes_tBD3126123F4810D8B272B8201C07FE70CDC9C5E2;
struct XmlDocument_t4DE82998E642C5C21A4A620A5278237C70D3E42B;
struct XmlDocumentType_tFD7379E45412FC2EBD65280ED3DE7A1C941A8D9E;
struct XmlElementList_tFE0D5337668FDE708796D7FDB669AE4A290CAB57;
struct XmlElementListEnumerator_tFF3D3868A2EFB7D39A510085383C371172943794;
struct XmlElementListListener_t1513DCDD9C9C5B56690342DCC4445CB4BE6E7E79;
struct XmlEmptyElementListEnumerator_tBA7DE21795A44BB9D31419C2CBB40A83D6DBEE84;
struct XmlImplementation_t4B3F467B76BD95C919C40424196C55B38EEC0F4D;
struct XmlLinkedNode_t640BF5D3FDAF0606665C3BAE565988A5014F1B9C;
struct XmlName_t0704430D24D202146901D342E34D878246E14F33;
struct XmlNameTable_tBDBAACFF3DB40A8E6AF3BDC11F0FF166CF11ABB8;
struct XmlNamedNodeMap_t13203D1B3861C62568AFFA1D644C04ABCBFC130C;
struct XmlNode_t3180B9B3D5C36CD58F5327D9F13458E3B3F030AF;
struct XmlNodeChangedEventArgs_t965F2E02F8433933D197A0D1AF1EFEEDB524DF3A;
struct XmlNodeChangedEventHandler_t907F6E9CB8DE238635028EC468AD41CAB0BA269B;
struct XmlNodeList_tFC0CEED3A006BEDF6A1A420F7CB55AC33E9B7F85;
struct XmlReader_t4C709DEF5F01606ECB60B638F1BD6F6E0A9116FD;
struct XmlSchemaAnnotated_t41DBE9DF8776C6C639B33A712F25497C672B4E04;
struct XmlSchemaAttribute_t04AF3100B886867E3F0495E22F44305EDEC5550F;
struct XmlSchemaElement_t24D5940E4EB4E5FFDAD1772AFBD7A83CD992C970;
struct XmlSchemaInfo_t6686870BCAA40A8EFD7987976F386DD40DD5323B;
struct XmlSchemaObject_t82CCD87AADB4BEF4E9DFE4C501F09EDA372AA19D;
struct XmlSchemaParticle_t9A7544A3F66C6E4B8900456CF705A6F9D95655EE;
struct XmlSchemaSimpleType_tA52BBE2D61DAD61BD3FCDF40E1CD9F41388F7FA8;
struct XmlSchemaType_t36630F421D730DDFA33CBA5BA6548094ABF40DDF;
struct EmptyParticle_tB70737785BCC28912378BA174C18956FF658454A;

IL2CPP_EXTERN_C RuntimeClass* ArgumentOutOfRangeException_tEA2822DAF62B10EEED00E0E3A341D4BAF78CF85F_il2cpp_TypeInfo_var;
IL2CPP_EXTERN_C RuntimeClass* ArrayList_t7A8E5AF0C4378015B5731ABE2BED8F2782FEEF8A_il2cpp_TypeInfo_var;
IL2CPP_EXTERN_C RuntimeClass* ByteU5BU5D_tA6237BF417AE52AD70CFB4EF24A7A82613DF9031_il2cpp_TypeInfo_var;
IL2CPP_EXTERN_C RuntimeClass* CharU5BU5D_t799905CF001DD5F13F7DBB310181FC4D8B7D0AAB_il2cpp_TypeInfo_var;
IL2CPP_EXTERN_C RuntimeClass* DebuggerDisplayXmlNodeProxy_t75382D048931DB9A7D8EFB35A56EEC1D8241FB07_il2cpp_TypeInfo_var;
IL2CPP_EXTERN_C RuntimeClass* Decimal_tDA6C877282B2D789CF97C0949661CC11D643969F_il2cpp_TypeInfo_var;
IL2CPP_EXTERN_C RuntimeClass* EmptyEnumerator_t84EC9187C8460EE98E675ED9258AE4DF2A6776DA_il2cpp_TypeInfo_var;
IL2CPP_EXTERN_C RuntimeClass* EmptyParticle_tB70737785BCC28912378BA174C18956FF658454A_il2cpp_TypeInfo_var;
IL2CPP_EXTERN_C RuntimeClass* GC_t920F9CF6EBB7C787E5010A4352E1B587F356DC58_il2cpp_TypeInfo_var;
IL2CPP_EXTERN_C RuntimeClass* Il2CppComObject_il2cpp_TypeInfo_var;
IL2CPP_EXTERN_C RuntimeClass* IndexOutOfRangeException_t7ECB35264FB6CA8FAA516BD958F4B2ADC78E8A82_il2cpp_TypeInfo_var;
IL2CPP_EXTERN_C RuntimeClass* InvalidOperationException_t5DDE4D49B7405FAAB1E4576F4715A42A3FAD4BAB_il2cpp_TypeInfo_var;
IL2CPP_EXTERN_C RuntimeClass* RuntimeObject_il2cpp_TypeInfo_var;
IL2CPP_EXTERN_C RuntimeClass* StringBuilder_t_il2cpp_TypeInfo_var;
IL2CPP_EXTERN_C RuntimeClass* StringU5BU5D_t7674CD946EC0CE7B3AE0BE70E6EE85F2ECD9F248_il2cpp_TypeInfo_var;
IL2CPP_EXTERN_C RuntimeClass* WeakReference_tD4B0518CE911FFD9FAAB3FCD492644A354312D8E_il2cpp_TypeInfo_var;
IL2CPP_EXTERN_C RuntimeClass* XmlAttribute_t4B6CC461196FBF5CC9F777E74CC82C98E0CA9D18_il2cpp_TypeInfo_var;
IL2CPP_EXTERN_C RuntimeClass* XmlCharType_t7C471894C8862126737E800F5A14AACBD14FCBC3_il2cpp_TypeInfo_var;
IL2CPP_EXTERN_C RuntimeClass* XmlChildEnumerator_tDD03B83EAA250AB86DFF4B53C1430DA942E85939_il2cpp_TypeInfo_var;
IL2CPP_EXTERN_C RuntimeClass* XmlChildNodes_tBD3126123F4810D8B272B8201C07FE70CDC9C5E2_il2cpp_TypeInfo_var;
IL2CPP_EXTERN_C RuntimeClass* XmlConvert_t5D100AFD62FF717BD4F8A6CABA9EDF7A5DF719EA_il2cpp_TypeInfo_var;
IL2CPP_EXTERN_C RuntimeClass* XmlDocumentType_tFD7379E45412FC2EBD65280ED3DE7A1C941A8D9E_il2cpp_TypeInfo_var;
IL2CPP_EXTERN_C RuntimeClass* XmlDocument_t4DE82998E642C5C21A4A620A5278237C70D3E42B_il2cpp_TypeInfo_var;
IL2CPP_EXTERN_C RuntimeClass* XmlElementListEnumerator_tFF3D3868A2EFB7D39A510085383C371172943794_il2cpp_TypeInfo_var;
IL2CPP_EXTERN_C RuntimeClass* XmlElementListListener_t1513DCDD9C9C5B56690342DCC4445CB4BE6E7E79_il2cpp_TypeInfo_var;
IL2CPP_EXTERN_C RuntimeClass* XmlElementList_tFE0D5337668FDE708796D7FDB669AE4A290CAB57_il2cpp_TypeInfo_var;
IL2CPP_EXTERN_C RuntimeClass* XmlEmptyElementListEnumerator_tBA7DE21795A44BB9D31419C2CBB40A83D6DBEE84_il2cpp_TypeInfo_var;
IL2CPP_EXTERN_C RuntimeClass* XmlLinkedNode_t640BF5D3FDAF0606665C3BAE565988A5014F1B9C_il2cpp_TypeInfo_var;
IL2CPP_EXTERN_C RuntimeClass* XmlNodeChangedEventHandler_t907F6E9CB8DE238635028EC468AD41CAB0BA269B_il2cpp_TypeInfo_var;
IL2CPP_EXTERN_C RuntimeClass* XmlNodeType_tCA25567E42A464038FDA6BA318EC0A9627B4CB95_il2cpp_TypeInfo_var;
IL2CPP_EXTERN_C RuntimeClass* XmlReaderDebuggerDisplayProxy_tCBCA1821E9EDC752D1884A7A4343C3B0C7134AC9_il2cpp_TypeInfo_var;
IL2CPP_EXTERN_C RuntimeClass* XmlReader_t4C709DEF5F01606ECB60B638F1BD6F6E0A9116FD_il2cpp_TypeInfo_var;
IL2CPP_EXTERN_C RuntimeClass* XmlSchemaInfo_t6686870BCAA40A8EFD7987976F386DD40DD5323B_il2cpp_TypeInfo_var;
IL2CPP_EXTERN_C RuntimeClass* XmlSchemaParticle_t9A7544A3F66C6E4B8900456CF705A6F9D95655EE_il2cpp_TypeInfo_var;
IL2CPP_EXTERN_C RuntimeField* DebuggerDisplayXmlNodeProxy_t75382D048931DB9A7D8EFB35A56EEC1D8241FB07____node_FieldInfo_var;
IL2CPP_EXTERN_C RuntimeField* U3CPrivateImplementationDetailsU3E_tBAB6DC40B1709E21296656960228A17F1BE57FD7____DACC0E6568D9B10337A1A693F10D42C3175E80C378154406C6FBFA989F919F51_FieldInfo_var;
IL2CPP_EXTERN_C RuntimeField* U3CPrivateImplementationDetailsU3E_tBAB6DC40B1709E21296656960228A17F1BE57FD7____DBBF94CCF4250B303976041627BB8FC302CE57D55373C469F41F97318412037D_FieldInfo_var;
IL2CPP_EXTERN_C RuntimeField* XmlReaderDebuggerDisplayProxy_tCBCA1821E9EDC752D1884A7A4343C3B0C7134AC9____reader_FieldInfo_var;
IL2CPP_EXTERN_C String_t* _stringLiteral02E5C3B8AD72F3FC46F6C829AB1FEDDC9B115473;
IL2CPP_EXTERN_C String_t* _stringLiteral0E0426234071F961E725D9438C6D5A048A0394B3;
IL2CPP_EXTERN_C String_t* _stringLiteral1F1BC4D23D8E5661364EBEC728BE303959F6CF91;
IL2CPP_EXTERN_C String_t* _stringLiteral2309CFA4F156DCCB12A14E727DCE560E1426B532;
IL2CPP_EXTERN_C String_t* _stringLiteral2B6D6F48C27C60C3B55391AB377D9DC8F5639AA1;
IL2CPP_EXTERN_C String_t* _stringLiteral2C444E82A2EF8AEC533AC89C8394A988DC706B88;
IL2CPP_EXTERN_C String_t* _stringLiteral2FF4A9E93E0170BF78F5B084F76C8B3194FAE523;
IL2CPP_EXTERN_C String_t* _stringLiteral3C67F6F3D5AD1DF5DD1E3B7F8583419767796154;
IL2CPP_EXTERN_C String_t* _stringLiteral3EF4F70676F5F18B8797A6624726002F607BB8DE;
IL2CPP_EXTERN_C String_t* _stringLiteral42646B33B50B6AA15E22733C8900716F0FE19E1D;
IL2CPP_EXTERN_C String_t* _stringLiteral785F17F45C331C415D0A7458E6AAC36966399C51;
IL2CPP_EXTERN_C String_t* _stringLiteral7F3238CD8C342B06FB9AB185C610175C84625462;
IL2CPP_EXTERN_C String_t* _stringLiteral81ABCB006928101D1882FA2FBB7BFA00FE053221;
IL2CPP_EXTERN_C String_t* _stringLiteral848E5ED630B3142F565DD995C6E8D30187ED33CD;
IL2CPP_EXTERN_C String_t* _stringLiteral876C4B39B6E4D0187090400768899C71D99DE90D;
IL2CPP_EXTERN_C String_t* _stringLiteral909AA3EFEECF6508CFB6F17EDD057508FF1D7D17;
IL2CPP_EXTERN_C String_t* _stringLiteral9465793DB5FEE411F38090ACC397B439BF6CC52C;
IL2CPP_EXTERN_C String_t* _stringLiteral94A9AE3A92C04F7400ADC94B7BCB73FFC0A08DE4;
IL2CPP_EXTERN_C String_t* _stringLiteral99EFCE42C9A728EA7C113BD7034366462E5E09E6;
IL2CPP_EXTERN_C String_t* _stringLiteral9FD9B00B05227B64D8B46A29B6D1B4FEA2C197EF;
IL2CPP_EXTERN_C String_t* _stringLiteralB78F235D4291950A7D101307609C259F3E1F033F;
IL2CPP_EXTERN_C String_t* _stringLiteralB7CFE1337753D3AE49222B506750148BBC1B1149;
IL2CPP_EXTERN_C String_t* _stringLiteralC62C64F00567C5368CAE37F4E64E1E82FF785677;
IL2CPP_EXTERN_C String_t* _stringLiteralCC5753B4554091FA687FA64F4FA303B0C913E2EC;
IL2CPP_EXTERN_C String_t* _stringLiteralD4DAA1A26807E1A567D68B161E6A95701A3FD560;
IL2CPP_EXTERN_C String_t* _stringLiteralE280D065A824A791F8305234D3E093FC9A5A90C7;
IL2CPP_EXTERN_C String_t* _stringLiteralFBD1A9745DCC4D80697D44756437CCB57698AE21;
IL2CPP_EXTERN_C String_t* _stringLiteralFDA1C9397356AF77AAE64869A9D6288562F10D4C;
IL2CPP_EXTERN_C const RuntimeMethod* EmptyEnumerator_System_Collections_IEnumerator_get_Current_m7F54B324E8E3410019E56521EE61CF97A7CED9B5_RuntimeMethod_var;
IL2CPP_EXTERN_C const RuntimeMethod* SmallXmlNodeList_get_Item_mFE828A47A297981D034EFC9244E53B3D34FC9576_RuntimeMethod_var;
IL2CPP_EXTERN_C const RuntimeMethod* XmlAttributeCollection_get_ItemOf_m8F968C39C03DFEB9998B2122F4A26B76B1DB2EDB_RuntimeMethod_var;
IL2CPP_EXTERN_C const RuntimeMethod* XmlChildEnumerator_get_Current_m149208EFC92AEDC42F82839102B3D5F017DA4C46_RuntimeMethod_var;
IL2CPP_EXTERN_C const RuntimeMethod* XmlElementListEnumerator_MoveNext_mAA954F4C42CDB8AF1602675835099FD6B5162ED1_RuntimeMethod_var;
IL2CPP_EXTERN_C const RuntimeMethod* XmlElementListListener_OnListChanged_mD4D9B710020444018B7CA3A06C295978B38943DD_RuntimeMethod_var;
IL2CPP_EXTERN_C const RuntimeType* DebuggerDisplayXmlNodeProxy_t75382D048931DB9A7D8EFB35A56EEC1D8241FB07_0_0_0_var;
IL2CPP_EXTERN_C const RuntimeType* XmlReaderDebuggerDisplayProxy_tCBCA1821E9EDC752D1884A7A4343C3B0C7134AC9_0_0_0_var;
struct Delegate_t_marshaled_com;
struct Delegate_t_marshaled_pinvoke;
struct Exception_t_marshaled_com;
struct Exception_t_marshaled_pinvoke;

struct ByteU5BU5D_tA6237BF417AE52AD70CFB4EF24A7A82613DF9031;
struct CharU5BU5D_t799905CF001DD5F13F7DBB310181FC4D8B7D0AAB;
struct DelegateU5BU5D_tC5AB7E8F745616680F337909D3A8E6C722CDF771;
struct StringU5BU5D_t7674CD946EC0CE7B3AE0BE70E6EE85F2ECD9F248;

IL2CPP_EXTERN_C_BEGIN
IL2CPP_EXTERN_C_END

#ifdef __clang__
#pragma clang diagnostic push
#pragma clang diagnostic ignored "-Winvalid-offsetof"
#pragma clang diagnostic ignored "-Wunused-variable"
#endif
struct U3CModuleU3E_t4791F64F4B6411D4D033A002CAD365D597AA2451 
{
};
struct U3CPrivateImplementationDetailsU3E_tBAB6DC40B1709E21296656960228A17F1BE57FD7  : public RuntimeObject
{
};
struct ArrayList_t7A8E5AF0C4378015B5731ABE2BED8F2782FEEF8A  : public RuntimeObject
{
	ObjectU5BU5D_t8061030B0A12A55D5AD8652A20C922FE99450918* ____items;
	int32_t ____size;
	int32_t ____version;
	RuntimeObject* ____syncRoot;
};
struct EmptyEnumerator_t84EC9187C8460EE98E675ED9258AE4DF2A6776DA  : public RuntimeObject
{
};
struct EventArgs_t37273F03EAC87217701DD431B190FBD84AD7C377  : public RuntimeObject
{
};
struct Ref_t4AE893B7D0F8A6F31DFD7F8B30847A9050BA5AFF  : public RuntimeObject
{
};
struct Res_t9D2D9AFBC694346FDF3C1FCBE2D6D7AAEADB3B9A  : public RuntimeObject
{
};
struct String_t  : public RuntimeObject
{
	int32_t ____stringLength;
	Il2CppChar ____firstChar;
};
struct StringBuilder_t  : public RuntimeObject
{
	CharU5BU5D_t799905CF001DD5F13F7DBB310181FC4D8B7D0AAB* ___m_ChunkChars;
	StringBuilder_t* ___m_ChunkPrevious;
	int32_t ___m_ChunkLength;
	int32_t ___m_ChunkOffset;
	int32_t ___m_MaxCapacity;
};
struct ValueType_t6D9B272BD21782F0A9A14F2E41F85A50E97A986F  : public RuntimeObject
{
};
struct ValueType_t6D9B272BD21782F0A9A14F2E41F85A50E97A986F_marshaled_pinvoke
{
};
struct ValueType_t6D9B272BD21782F0A9A14F2E41F85A50E97A986F_marshaled_com
{
};
struct XmlChildEnumerator_tDD03B83EAA250AB86DFF4B53C1430DA942E85939  : public RuntimeObject
{
	XmlNode_t3180B9B3D5C36CD58F5327D9F13458E3B3F030AF* ___container;
	XmlNode_t3180B9B3D5C36CD58F5327D9F13458E3B3F030AF* ___child;
	bool ___isFirst;
};
struct XmlConvert_t5D100AFD62FF717BD4F8A6CABA9EDF7A5DF719EA  : public RuntimeObject
{
};
struct XmlElementListEnumerator_tFF3D3868A2EFB7D39A510085383C371172943794  : public RuntimeObject
{
	XmlElementList_tFE0D5337668FDE708796D7FDB669AE4A290CAB57* ___list;
	XmlNode_t3180B9B3D5C36CD58F5327D9F13458E3B3F030AF* ___curElem;
	int32_t ___changeCount;
};
struct XmlElementListListener_t1513DCDD9C9C5B56690342DCC4445CB4BE6E7E79  : public RuntimeObject
{
	WeakReference_tD4B0518CE911FFD9FAAB3FCD492644A354312D8E* ___elemList;
	XmlDocument_t4DE82998E642C5C21A4A620A5278237C70D3E42B* ___doc;
	XmlNodeChangedEventHandler_t907F6E9CB8DE238635028EC468AD41CAB0BA269B* ___nodeChangeHandler;
};
struct XmlEmptyElementListEnumerator_tBA7DE21795A44BB9D31419C2CBB40A83D6DBEE84  : public RuntimeObject
{
};
struct XmlImplementation_t4B3F467B76BD95C919C40424196C55B38EEC0F4D  : public RuntimeObject
{
	XmlNameTable_tBDBAACFF3DB40A8E6AF3BDC11F0FF166CF11ABB8* ___nameTable;
};
struct XmlName_t0704430D24D202146901D342E34D878246E14F33  : public RuntimeObject
{
	String_t* ___prefix;
	String_t* ___localName;
	String_t* ___name;
	int32_t ___hashCode;
	XmlDocument_t4DE82998E642C5C21A4A620A5278237C70D3E42B* ___ownerDoc;
};
struct XmlNameTable_tBDBAACFF3DB40A8E6AF3BDC11F0FF166CF11ABB8  : public RuntimeObject
{
};
struct XmlNode_t3180B9B3D5C36CD58F5327D9F13458E3B3F030AF  : public RuntimeObject
{
	XmlNode_t3180B9B3D5C36CD58F5327D9F13458E3B3F030AF* ___parentNode;
};
struct XmlNodeList_tFC0CEED3A006BEDF6A1A420F7CB55AC33E9B7F85  : public RuntimeObject
{
};
struct XmlReader_t4C709DEF5F01606ECB60B638F1BD6F6E0A9116FD  : public RuntimeObject
{
};
struct XmlSchemaObject_t82CCD87AADB4BEF4E9DFE4C501F09EDA372AA19D  : public RuntimeObject
{
};
struct Boolean_t09A6377A54BE2F9E6985A8149F19234FD7DDFE22 
{
	bool ___m_value;
};
struct Byte_t94D9231AC217BE4D2E004C4CD32DF6D099EA41A3 
{
	uint8_t ___m_value;
};
struct Char_t521A6F19B456D956AF452D926C32709DC03D6B17 
{
	Il2CppChar ___m_value;
};
struct DebuggerDisplayXmlNodeProxy_t75382D048931DB9A7D8EFB35A56EEC1D8241FB07 
{
	XmlNode_t3180B9B3D5C36CD58F5327D9F13458E3B3F030AF* ___node;
};
struct DebuggerDisplayXmlNodeProxy_t75382D048931DB9A7D8EFB35A56EEC1D8241FB07_marshaled_pinvoke
{
	XmlNode_t3180B9B3D5C36CD58F5327D9F13458E3B3F030AF* ___node;
};
struct DebuggerDisplayXmlNodeProxy_t75382D048931DB9A7D8EFB35A56EEC1D8241FB07_marshaled_com
{
	XmlNode_t3180B9B3D5C36CD58F5327D9F13458E3B3F030AF* ___node;
};
struct Decimal_tDA6C877282B2D789CF97C0949661CC11D643969F 
{
	union
	{
		#pragma pack(push, tp, 1)
		struct
		{
			int32_t ___flags;
		};
		#pragma pack(pop, tp)
		struct
		{
			int32_t ___flags_forAlignmentOnly;
		};
		#pragma pack(push, tp, 1)
		struct
		{
			char ___hi_OffsetPadding[4];
			int32_t ___hi;
		};
		#pragma pack(pop, tp)
		struct
		{
			char ___hi_OffsetPadding_forAlignmentOnly[4];
			int32_t ___hi_forAlignmentOnly;
		};
		#pragma pack(push, tp, 1)
		struct
		{
			char ___lo_OffsetPadding[8];
			int32_t ___lo;
		};
		#pragma pack(pop, tp)
		struct
		{
			char ___lo_OffsetPadding_forAlignmentOnly[8];
			int32_t ___lo_forAlignmentOnly;
		};
		#pragma pack(push, tp, 1)
		struct
		{
			char ___mid_OffsetPadding[12];
			int32_t ___mid;
		};
		#pragma pack(pop, tp)
		struct
		{
			char ___mid_OffsetPadding_forAlignmentOnly[12];
			int32_t ___mid_forAlignmentOnly;
		};
		#pragma pack(push, tp, 1)
		struct
		{
			char ___ulomidLE_OffsetPadding[8];
			uint64_t ___ulomidLE;
		};
		#pragma pack(pop, tp)
		struct
		{
			char ___ulomidLE_OffsetPadding_forAlignmentOnly[8];
			uint64_t ___ulomidLE_forAlignmentOnly;
		};
	};
};
struct Enum_t2A1A94B24E3B776EEF4E5E485E290BB9D4D072E2  : public ValueType_t6D9B272BD21782F0A9A14F2E41F85A50E97A986F
{
};
struct Enum_t2A1A94B24E3B776EEF4E5E485E290BB9D4D072E2_marshaled_pinvoke
{
};
struct Enum_t2A1A94B24E3B776EEF4E5E485E290BB9D4D072E2_marshaled_com
{
};
struct Int32_t680FF22E76F6EFAD4375103CBBFFA0421349384C 
{
	int32_t ___m_value;
};
struct IntPtr_t 
{
	void* ___m_value;
};
struct UInt32_t1833D51FFA667B18A5AA4B8D34DE284F8495D29B 
{
	uint32_t ___m_value;
};
struct Void_t4861ACF8F4594C3437BB48B6E56783494B843915 
{
	union
	{
		struct
		{
		};
		uint8_t Void_t4861ACF8F4594C3437BB48B6E56783494B843915__padding[1];
	};
};
struct XmlAttribute_t4B6CC461196FBF5CC9F777E74CC82C98E0CA9D18  : public XmlNode_t3180B9B3D5C36CD58F5327D9F13458E3B3F030AF
{
	XmlName_t0704430D24D202146901D342E34D878246E14F33* ___name;
};
struct XmlCharType_t7C471894C8862126737E800F5A14AACBD14FCBC3 
{
	ByteU5BU5D_tA6237BF417AE52AD70CFB4EF24A7A82613DF9031* ___charProperties;
};
struct XmlCharType_t7C471894C8862126737E800F5A14AACBD14FCBC3_marshaled_pinvoke
{
	Il2CppSafeArray* ___charProperties;
};
struct XmlCharType_t7C471894C8862126737E800F5A14AACBD14FCBC3_marshaled_com
{
	Il2CppSafeArray* ___charProperties;
};
struct XmlChildNodes_tBD3126123F4810D8B272B8201C07FE70CDC9C5E2  : public XmlNodeList_tFC0CEED3A006BEDF6A1A420F7CB55AC33E9B7F85
{
	XmlNode_t3180B9B3D5C36CD58F5327D9F13458E3B3F030AF* ___container;
};
struct XmlDocument_t4DE82998E642C5C21A4A620A5278237C70D3E42B  : public XmlNode_t3180B9B3D5C36CD58F5327D9F13458E3B3F030AF
{
	XmlImplementation_t4B3F467B76BD95C919C40424196C55B38EEC0F4D* ___implementation;
	XmlNodeChangedEventHandler_t907F6E9CB8DE238635028EC468AD41CAB0BA269B* ___onNodeInsertedDelegate;
	XmlNodeChangedEventHandler_t907F6E9CB8DE238635028EC468AD41CAB0BA269B* ___onNodeRemovedDelegate;
	String_t* ___strDocumentName;
};
struct XmlElementList_tFE0D5337668FDE708796D7FDB669AE4A290CAB57  : public XmlNodeList_tFC0CEED3A006BEDF6A1A420F7CB55AC33E9B7F85
{
	String_t* ___asterisk;
	int32_t ___changeCount;
	String_t* ___name;
	String_t* ___localName;
	String_t* ___namespaceURI;
	XmlNode_t3180B9B3D5C36CD58F5327D9F13458E3B3F030AF* ___rootNode;
	int32_t ___curInd;
	XmlNode_t3180B9B3D5C36CD58F5327D9F13458E3B3F030AF* ___curElem;
	bool ___empty;
	bool ___atomized;
	int32_t ___matchCount;
	WeakReference_tD4B0518CE911FFD9FAAB3FCD492644A354312D8E* ___listener;
};
struct XmlLinkedNode_t640BF5D3FDAF0606665C3BAE565988A5014F1B9C  : public XmlNode_t3180B9B3D5C36CD58F5327D9F13458E3B3F030AF
{
	XmlLinkedNode_t640BF5D3FDAF0606665C3BAE565988A5014F1B9C* ___next;
};
struct XmlSchemaAnnotated_t41DBE9DF8776C6C639B33A712F25497C672B4E04  : public XmlSchemaObject_t82CCD87AADB4BEF4E9DFE4C501F09EDA372AA19D
{
};
#pragma pack(push, tp, 1)
struct __StaticArrayInitTypeSizeU3D6_t6B2CCEB1D7CF8B1F02780951EBD798284272BFE4 
{
	union
	{
		struct
		{
			union
			{
			};
		};
		uint8_t __StaticArrayInitTypeSizeU3D6_t6B2CCEB1D7CF8B1F02780951EBD798284272BFE4__padding[6];
	};
};
#pragma pack(pop, tp)
struct SmallXmlNodeList_tAF536F4BFCA26BF1B0E912818E921A3181D49EF0 
{
	RuntimeObject* ___field;
};
struct SmallXmlNodeList_tAF536F4BFCA26BF1B0E912818E921A3181D49EF0_marshaled_pinvoke
{
	Il2CppIUnknown* ___field;
};
struct SmallXmlNodeList_tAF536F4BFCA26BF1B0E912818E921A3181D49EF0_marshaled_com
{
	Il2CppIUnknown* ___field;
};
struct XmlReaderDebuggerDisplayProxy_tCBCA1821E9EDC752D1884A7A4343C3B0C7134AC9 
{
	XmlReader_t4C709DEF5F01606ECB60B638F1BD6F6E0A9116FD* ___reader;
};
struct XmlReaderDebuggerDisplayProxy_tCBCA1821E9EDC752D1884A7A4343C3B0C7134AC9_marshaled_pinvoke
{
	XmlReader_t4C709DEF5F01606ECB60B638F1BD6F6E0A9116FD* ___reader;
};
struct XmlReaderDebuggerDisplayProxy_tCBCA1821E9EDC752D1884A7A4343C3B0C7134AC9_marshaled_com
{
	XmlReader_t4C709DEF5F01606ECB60B638F1BD6F6E0A9116FD* ___reader;
};
struct Delegate_t  : public RuntimeObject
{
	intptr_t ___method_ptr;
	intptr_t ___invoke_impl;
	RuntimeObject* ___m_target;
	intptr_t ___method;
	intptr_t ___delegate_trampoline;
	intptr_t ___extra_arg;
	intptr_t ___method_code;
	intptr_t ___interp_method;
	intptr_t ___interp_invoke_impl;
	MethodInfo_t* ___method_info;
	MethodInfo_t* ___original_method_info;
	DelegateData_t9B286B493293CD2D23A5B2B5EF0E5B1324C2B77E* ___data;
	bool ___method_is_virtual;
};
struct Delegate_t_marshaled_pinvoke
{
	intptr_t ___method_ptr;
	intptr_t ___invoke_impl;
	Il2CppIUnknown* ___m_target;
	intptr_t ___method;
	intptr_t ___delegate_trampoline;
	intptr_t ___extra_arg;
	intptr_t ___method_code;
	intptr_t ___interp_method;
	intptr_t ___interp_invoke_impl;
	MethodInfo_t* ___method_info;
	MethodInfo_t* ___original_method_info;
	DelegateData_t9B286B493293CD2D23A5B2B5EF0E5B1324C2B77E* ___data;
	int32_t ___method_is_virtual;
};
struct Delegate_t_marshaled_com
{
	intptr_t ___method_ptr;
	intptr_t ___invoke_impl;
	Il2CppIUnknown* ___m_target;
	intptr_t ___method;
	intptr_t ___delegate_trampoline;
	intptr_t ___extra_arg;
	intptr_t ___method_code;
	intptr_t ___interp_method;
	intptr_t ___interp_invoke_impl;
	MethodInfo_t* ___method_info;
	MethodInfo_t* ___original_method_info;
	DelegateData_t9B286B493293CD2D23A5B2B5EF0E5B1324C2B77E* ___data;
	int32_t ___method_is_virtual;
};
struct Exception_t  : public RuntimeObject
{
	String_t* ____className;
	String_t* ____message;
	RuntimeObject* ____data;
	Exception_t* ____innerException;
	String_t* ____helpURL;
	RuntimeObject* ____stackTrace;
	String_t* ____stackTraceString;
	String_t* ____remoteStackTraceString;
	int32_t ____remoteStackIndex;
	RuntimeObject* ____dynamicMethods;
	int32_t ____HResult;
	String_t* ____source;
	SafeSerializationManager_tCBB85B95DFD1634237140CD892E82D06ECB3F5E6* ____safeSerializationManager;
	StackTraceU5BU5D_t32FBCB20930EAF5BAE3F450FF75228E5450DA0DF* ___captured_traces;
	IntPtrU5BU5D_tFD177F8C806A6921AD7150264CCC62FA00CAD832* ___native_trace_ips;
	int32_t ___caught_in_unmanaged;
};
struct Exception_t_marshaled_pinvoke
{
	char* ____className;
	char* ____message;
	RuntimeObject* ____data;
	Exception_t_marshaled_pinvoke* ____innerException;
	char* ____helpURL;
	Il2CppIUnknown* ____stackTrace;
	char* ____stackTraceString;
	char* ____remoteStackTraceString;
	int32_t ____remoteStackIndex;
	Il2CppIUnknown* ____dynamicMethods;
	int32_t ____HResult;
	char* ____source;
	SafeSerializationManager_tCBB85B95DFD1634237140CD892E82D06ECB3F5E6* ____safeSerializationManager;
	StackTraceU5BU5D_t32FBCB20930EAF5BAE3F450FF75228E5450DA0DF* ___captured_traces;
	Il2CppSafeArray* ___native_trace_ips;
	int32_t ___caught_in_unmanaged;
};
struct Exception_t_marshaled_com
{
	Il2CppChar* ____className;
	Il2CppChar* ____message;
	RuntimeObject* ____data;
	Exception_t_marshaled_com* ____innerException;
	Il2CppChar* ____helpURL;
	Il2CppIUnknown* ____stackTrace;
	Il2CppChar* ____stackTraceString;
	Il2CppChar* ____remoteStackTraceString;
	int32_t ____remoteStackIndex;
	Il2CppIUnknown* ____dynamicMethods;
	int32_t ____HResult;
	Il2CppChar* ____source;
	SafeSerializationManager_tCBB85B95DFD1634237140CD892E82D06ECB3F5E6* ____safeSerializationManager;
	StackTraceU5BU5D_t32FBCB20930EAF5BAE3F450FF75228E5450DA0DF* ___captured_traces;
	Il2CppSafeArray* ___native_trace_ips;
	int32_t ___caught_in_unmanaged;
};
struct GCHandle_tC44F6F72EE68BD4CFABA24309DA7A179D41127DC 
{
	intptr_t ___handle;
};
struct RuntimeFieldHandle_t6E4C45B6D2EA12FC99185805A7E77527899B25C5 
{
	intptr_t ___value;
};
struct XmlDocumentType_tFD7379E45412FC2EBD65280ED3DE7A1C941A8D9E  : public XmlLinkedNode_t640BF5D3FDAF0606665C3BAE565988A5014F1B9C
{
	String_t* ___name;
	String_t* ___publicId;
	String_t* ___systemId;
	String_t* ___internalSubset;
};
struct XmlNamedNodeMap_t13203D1B3861C62568AFFA1D644C04ABCBFC130C  : public RuntimeObject
{
	SmallXmlNodeList_tAF536F4BFCA26BF1B0E912818E921A3181D49EF0 ___nodes;
};
struct XmlNodeChangedAction_tDF172D469BF61A7AF723976395070B130A65A233 
{
	int32_t ___value__;
};
struct XmlNodeType_tCA25567E42A464038FDA6BA318EC0A9627B4CB95 
{
	int32_t ___value__;
};
struct XmlSchemaAttribute_t04AF3100B886867E3F0495E22F44305EDEC5550F  : public XmlSchemaAnnotated_t41DBE9DF8776C6C639B33A712F25497C672B4E04
{
};
struct XmlSchemaContentType_t30F65D76DF78B49262A67031AF1B166EDF032600 
{
	int32_t ___value__;
};
struct XmlSchemaParticle_t9A7544A3F66C6E4B8900456CF705A6F9D95655EE  : public XmlSchemaAnnotated_t41DBE9DF8776C6C639B33A712F25497C672B4E04
{
	Decimal_tDA6C877282B2D789CF97C0949661CC11D643969F ___minOccurs;
	Decimal_tDA6C877282B2D789CF97C0949661CC11D643969F ___maxOccurs;
};
struct XmlSchemaType_t36630F421D730DDFA33CBA5BA6548094ABF40DDF  : public XmlSchemaAnnotated_t41DBE9DF8776C6C639B33A712F25497C672B4E04
{
};
struct XmlSchemaValidity_t6A23BF5BB6AF73795630FCCCF46F0F600BA8B4F6 
{
	int32_t ___value__;
};
struct MulticastDelegate_t  : public Delegate_t
{
	DelegateU5BU5D_tC5AB7E8F745616680F337909D3A8E6C722CDF771* ___delegates;
};
struct MulticastDelegate_t_marshaled_pinvoke : public Delegate_t_marshaled_pinvoke
{
	Delegate_t_marshaled_pinvoke** ___delegates;
};
struct MulticastDelegate_t_marshaled_com : public Delegate_t_marshaled_com
{
	Delegate_t_marshaled_com** ___delegates;
};
struct SystemException_tCC48D868298F4C0705279823E34B00F4FBDB7295  : public Exception_t
{
};
struct WeakReference_tD4B0518CE911FFD9FAAB3FCD492644A354312D8E  : public RuntimeObject
{
	bool ___isLongReference;
	GCHandle_tC44F6F72EE68BD4CFABA24309DA7A179D41127DC ___gcHandle;
};
struct XmlAttributeCollection_tB0BA25B3C7E8D4BCF487C7107A9019632A7D85C0  : public XmlNamedNodeMap_t13203D1B3861C62568AFFA1D644C04ABCBFC130C
{
};
struct XmlNodeChangedEventArgs_t965F2E02F8433933D197A0D1AF1EFEEDB524DF3A  : public EventArgs_t37273F03EAC87217701DD431B190FBD84AD7C377
{
	int32_t ___action;
	XmlNode_t3180B9B3D5C36CD58F5327D9F13458E3B3F030AF* ___node;
};
struct XmlSchemaElement_t24D5940E4EB4E5FFDAD1772AFBD7A83CD992C970  : public XmlSchemaParticle_t9A7544A3F66C6E4B8900456CF705A6F9D95655EE
{
};
struct XmlSchemaInfo_t6686870BCAA40A8EFD7987976F386DD40DD5323B  : public RuntimeObject
{
	bool ___isDefault;
	bool ___isNil;
	XmlSchemaElement_t24D5940E4EB4E5FFDAD1772AFBD7A83CD992C970* ___schemaElement;
	XmlSchemaAttribute_t04AF3100B886867E3F0495E22F44305EDEC5550F* ___schemaAttribute;
	XmlSchemaType_t36630F421D730DDFA33CBA5BA6548094ABF40DDF* ___schemaType;
	XmlSchemaSimpleType_tA52BBE2D61DAD61BD3FCDF40E1CD9F41388F7FA8* ___memberType;
	int32_t ___validity;
	int32_t ___contentType;
};
struct XmlSchemaSimpleType_tA52BBE2D61DAD61BD3FCDF40E1CD9F41388F7FA8  : public XmlSchemaType_t36630F421D730DDFA33CBA5BA6548094ABF40DDF
{
};
struct EmptyParticle_tB70737785BCC28912378BA174C18956FF658454A  : public XmlSchemaParticle_t9A7544A3F66C6E4B8900456CF705A6F9D95655EE
{
};
struct ArgumentException_tAD90411542A20A9C72D5CDA3A84181D8B947A263  : public SystemException_tCC48D868298F4C0705279823E34B00F4FBDB7295
{
	String_t* ____paramName;
};
struct IndexOutOfRangeException_t7ECB35264FB6CA8FAA516BD958F4B2ADC78E8A82  : public SystemException_tCC48D868298F4C0705279823E34B00F4FBDB7295
{
};
struct InvalidOperationException_t5DDE4D49B7405FAAB1E4576F4715A42A3FAD4BAB  : public SystemException_tCC48D868298F4C0705279823E34B00F4FBDB7295
{
};
struct XmlNodeChangedEventHandler_t907F6E9CB8DE238635028EC468AD41CAB0BA269B  : public MulticastDelegate_t
{
};
struct ArgumentOutOfRangeException_tEA2822DAF62B10EEED00E0E3A341D4BAF78CF85F  : public ArgumentException_tAD90411542A20A9C72D5CDA3A84181D8B947A263
{
	RuntimeObject* ____actualValue;
};
struct U3CPrivateImplementationDetailsU3E_tBAB6DC40B1709E21296656960228A17F1BE57FD7_StaticFields
{
	int64_t ___DACC0E6568D9B10337A1A693F10D42C3175E80C378154406C6FBFA989F919F51;
	__StaticArrayInitTypeSizeU3D6_t6B2CCEB1D7CF8B1F02780951EBD798284272BFE4 ___DBBF94CCF4250B303976041627BB8FC302CE57D55373C469F41F97318412037D;
};
struct String_t_StaticFields
{
	String_t* ___Empty;
};
struct XmlConvert_t5D100AFD62FF717BD4F8A6CABA9EDF7A5DF719EA_StaticFields
{
	XmlCharType_t7C471894C8862126737E800F5A14AACBD14FCBC3 ___xmlCharType;
	CharU5BU5D_t799905CF001DD5F13F7DBB310181FC4D8B7D0AAB* ___crt;
	int32_t ___c_EncodedCharLength;
	CharU5BU5D_t799905CF001DD5F13F7DBB310181FC4D8B7D0AAB* ___WhitespaceChars;
};
struct XmlReader_t4C709DEF5F01606ECB60B638F1BD6F6E0A9116FD_StaticFields
{
	uint32_t ___IsTextualNodeBitmap;
	uint32_t ___CanReadContentAsBitmap;
	uint32_t ___HasValueBitmap;
};
struct Boolean_t09A6377A54BE2F9E6985A8149F19234FD7DDFE22_StaticFields
{
	String_t* ___TrueString;
	String_t* ___FalseString;
};
struct Char_t521A6F19B456D956AF452D926C32709DC03D6B17_StaticFields
{
	ByteU5BU5D_tA6237BF417AE52AD70CFB4EF24A7A82613DF9031* ___s_categoryForLatin1;
};
struct Decimal_tDA6C877282B2D789CF97C0949661CC11D643969F_StaticFields
{
	Decimal_tDA6C877282B2D789CF97C0949661CC11D643969F ___Zero;
	Decimal_tDA6C877282B2D789CF97C0949661CC11D643969F ___One;
	Decimal_tDA6C877282B2D789CF97C0949661CC11D643969F ___MinusOne;
	Decimal_tDA6C877282B2D789CF97C0949661CC11D643969F ___MaxValue;
	Decimal_tDA6C877282B2D789CF97C0949661CC11D643969F ___MinValue;
};
struct Enum_t2A1A94B24E3B776EEF4E5E485E290BB9D4D072E2_StaticFields
{
	CharU5BU5D_t799905CF001DD5F13F7DBB310181FC4D8B7D0AAB* ___enumSeperatorCharArray;
};
struct IntPtr_t_StaticFields
{
	intptr_t ___Zero;
};
struct XmlCharType_t7C471894C8862126737E800F5A14AACBD14FCBC3_StaticFields
{
	RuntimeObject* ___s_Lock;
	ByteU5BU5D_tA6237BF417AE52AD70CFB4EF24A7A82613DF9031* ___s_CharProperties;
};
struct XmlDocument_t4DE82998E642C5C21A4A620A5278237C70D3E42B_StaticFields
{
	EmptyEnumerator_t84EC9187C8460EE98E675ED9258AE4DF2A6776DA* ___EmptyEnumerator;
	RuntimeObject* ___NotKnownSchemaInfo;
	RuntimeObject* ___ValidSchemaInfo;
	RuntimeObject* ___InvalidSchemaInfo;
};
struct XmlSchemaParticle_t9A7544A3F66C6E4B8900456CF705A6F9D95655EE_StaticFields
{
	XmlSchemaParticle_t9A7544A3F66C6E4B8900456CF705A6F9D95655EE* ___Empty;
};
#ifdef __clang__
#pragma clang diagnostic pop
#endif
struct StringU5BU5D_t7674CD946EC0CE7B3AE0BE70E6EE85F2ECD9F248  : public RuntimeArray
{
	ALIGN_FIELD (8) String_t* m_Items[1];

	inline String_t* GetAt(il2cpp_array_size_t index) const
	{
		IL2CPP_ARRAY_BOUNDS_CHECK(index, (uint32_t)(this)->max_length);
		return m_Items[index];
	}
	inline String_t** GetAddressAt(il2cpp_array_size_t index)
	{
		IL2CPP_ARRAY_BOUNDS_CHECK(index, (uint32_t)(this)->max_length);
		return m_Items + index;
	}
	inline void SetAt(il2cpp_array_size_t index, String_t* value)
	{
		IL2CPP_ARRAY_BOUNDS_CHECK(index, (uint32_t)(this)->max_length);
		m_Items[index] = value;
		Il2CppCodeGenWriteBarrier((void**)m_Items + index, (void*)value);
	}
	inline String_t* GetAtUnchecked(il2cpp_array_size_t index) const
	{
		return m_Items[index];
	}
	inline String_t** GetAddressAtUnchecked(il2cpp_array_size_t index)
	{
		return m_Items + index;
	}
	inline void SetAtUnchecked(il2cpp_array_size_t index, String_t* value)
	{
		m_Items[index] = value;
		Il2CppCodeGenWriteBarrier((void**)m_Items + index, (void*)value);
	}
};
struct DelegateU5BU5D_tC5AB7E8F745616680F337909D3A8E6C722CDF771  : public RuntimeArray
{
	ALIGN_FIELD (8) Delegate_t* m_Items[1];

	inline Delegate_t* GetAt(il2cpp_array_size_t index) const
	{
		IL2CPP_ARRAY_BOUNDS_CHECK(index, (uint32_t)(this)->max_length);
		return m_Items[index];
	}
	inline Delegate_t** GetAddressAt(il2cpp_array_size_t index)
	{
		IL2CPP_ARRAY_BOUNDS_CHECK(index, (uint32_t)(this)->max_length);
		return m_Items + index;
	}
	inline void SetAt(il2cpp_array_size_t index, Delegate_t* value)
	{
		IL2CPP_ARRAY_BOUNDS_CHECK(index, (uint32_t)(this)->max_length);
		m_Items[index] = value;
		Il2CppCodeGenWriteBarrier((void**)m_Items + index, (void*)value);
	}
	inline Delegate_t* GetAtUnchecked(il2cpp_array_size_t index) const
	{
		return m_Items[index];
	}
	inline Delegate_t** GetAddressAtUnchecked(il2cpp_array_size_t index)
	{
		return m_Items + index;
	}
	inline void SetAtUnchecked(il2cpp_array_size_t index, Delegate_t* value)
	{
		m_Items[index] = value;
		Il2CppCodeGenWriteBarrier((void**)m_Items + index, (void*)value);
	}
};
struct ByteU5BU5D_tA6237BF417AE52AD70CFB4EF24A7A82613DF9031  : public RuntimeArray
{
	ALIGN_FIELD (8) uint8_t m_Items[1];

	inline uint8_t GetAt(il2cpp_array_size_t index) const
	{
		IL2CPP_ARRAY_BOUNDS_CHECK(index, (uint32_t)(this)->max_length);
		return m_Items[index];
	}
	inline uint8_t* GetAddressAt(il2cpp_array_size_t index)
	{
		IL2CPP_ARRAY_BOUNDS_CHECK(index, (uint32_t)(this)->max_length);
		return m_Items + index;
	}
	inline void SetAt(il2cpp_array_size_t index, uint8_t value)
	{
		IL2CPP_ARRAY_BOUNDS_CHECK(index, (uint32_t)(this)->max_length);
		m_Items[index] = value;
	}
	inline uint8_t GetAtUnchecked(il2cpp_array_size_t index) const
	{
		return m_Items[index];
	}
	inline uint8_t* GetAddressAtUnchecked(il2cpp_array_size_t index)
	{
		return m_Items + index;
	}
	inline void SetAtUnchecked(il2cpp_array_size_t index, uint8_t value)
	{
		m_Items[index] = value;
	}
};
struct CharU5BU5D_t799905CF001DD5F13F7DBB310181FC4D8B7D0AAB  : public RuntimeArray
{
	ALIGN_FIELD (8) Il2CppChar m_Items[1];

	inline Il2CppChar GetAt(il2cpp_array_size_t index) const
	{
		IL2CPP_ARRAY_BOUNDS_CHECK(index, (uint32_t)(this)->max_length);
		return m_Items[index];
	}
	inline Il2CppChar* GetAddressAt(il2cpp_array_size_t index)
	{
		IL2CPP_ARRAY_BOUNDS_CHECK(index, (uint32_t)(this)->max_length);
		return m_Items + index;
	}
	inline void SetAt(il2cpp_array_size_t index, Il2CppChar value)
	{
		IL2CPP_ARRAY_BOUNDS_CHECK(index, (uint32_t)(this)->max_length);
		m_Items[index] = value;
	}
	inline Il2CppChar GetAtUnchecked(il2cpp_array_size_t index) const
	{
		return m_Items[index];
	}
	inline Il2CppChar* GetAddressAtUnchecked(il2cpp_array_size_t index)
	{
		return m_Items + index;
	}
	inline void SetAtUnchecked(il2cpp_array_size_t index, Il2CppChar value)
	{
		m_Items[index] = value;
	}
};



IL2CPP_MANAGED_FORCE_INLINE IL2CPP_METHOD_ATTR int32_t String_get_Length_m42625D67623FA5CC7A44D47425CE86FB946542D2_inline (String_t* __this, const RuntimeMethod* method) ;
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR String_t* String_Concat_m8855A6DE10F84DA7F4EC113CADDB59873A25573B (String_t* ___0_str0, String_t* ___1_str1, String_t* ___2_str2, const RuntimeMethod* method) ;
IL2CPP_MANAGED_FORCE_INLINE IL2CPP_METHOD_ATTR void XmlReaderDebuggerDisplayProxy__ctor_m56B82747E748FBB7BC8D0A1728D526EA23B54C1A_inline (XmlReaderDebuggerDisplayProxy_tCBCA1821E9EDC752D1884A7A4343C3B0C7134AC9* __this, XmlReader_t4C709DEF5F01606ECB60B638F1BD6F6E0A9116FD* ___0_reader, const RuntimeMethod* method) ;
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR String_t* Enum_ToString_m946B0B83C4470457D0FF555D862022C72BB55741 (RuntimeObject* __this, const RuntimeMethod* method) ;
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR String_t* String_Concat_m093934F71A9B351911EE46311674ED463B180006 (String_t* ___0_str0, String_t* ___1_str1, String_t* ___2_str2, String_t* ___3_str3, const RuntimeMethod* method) ;
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR String_t* XmlConvert_EscapeValueForDebuggerDisplay_m4290160B46B7BA2841826618DBBD1403371CA202 (String_t* ___0_value, const RuntimeMethod* method) ;
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR String_t* String_Concat_m647EBF831F54B6DF7D5AFA5FD012CF4EE7571B6A (StringU5BU5D_t7674CD946EC0CE7B3AE0BE70E6EE85F2ECD9F248* ___0_values, const RuntimeMethod* method) ;
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR String_t* XmlReaderDebuggerDisplayProxy_ToString_mEBDAEFA455D93E9934990687C8ED5421E6537FFB (XmlReaderDebuggerDisplayProxy_tCBCA1821E9EDC752D1884A7A4343C3B0C7134AC9* __this, const RuntimeMethod* method) ;
IL2CPP_MANAGED_FORCE_INLINE IL2CPP_METHOD_ATTR int32_t XmlName_get_HashCode_m329A9FEAF3BDEAA5F1F40CB84F7E51E1BDE58B2D_inline (XmlName_t0704430D24D202146901D342E34D878246E14F33* __this, const RuntimeMethod* method) ;
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR String_t* XmlName_get_Name_m77B33DD36549E6E3E728EA1AD3325E339EC30435 (XmlName_t0704430D24D202146901D342E34D878246E14F33* __this, const RuntimeMethod* method) ;
IL2CPP_MANAGED_FORCE_INLINE IL2CPP_METHOD_ATTR String_t* XmlName_get_LocalName_mD4DE8695A21CB393834F0DDFDD6D6B610E91A64D_inline (XmlName_t0704430D24D202146901D342E34D878246E14F33* __this, const RuntimeMethod* method) ;
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR RuntimeObject* SmallXmlNodeList_get_Item_mFE828A47A297981D034EFC9244E53B3D34FC9576 (SmallXmlNodeList_tAF536F4BFCA26BF1B0E912818E921A3181D49EF0* __this, int32_t ___0_index, const RuntimeMethod* method) ;
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR String_t* Res_GetString_mBEE82ACFE97F93A6DDE81E322477CF2713466364 (String_t* ___0_name, const RuntimeMethod* method) ;
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR void IndexOutOfRangeException__ctor_mFD06819F05B815BE2D6E826D4E04F4C449D0A425 (IndexOutOfRangeException_t7ECB35264FB6CA8FAA516BD958F4B2ADC78E8A82* __this, String_t* ___0_message, const RuntimeMethod* method) ;
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR int32_t XmlName_GetHashCode_mC2C5513D62B0E10E556D1241BC33C4AFFAD1DDCD (String_t* ___0_name, const RuntimeMethod* method) ;
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR int32_t XmlAttribute_get_LocalNameHash_m09745CA0C4CEA4D07864D977FF90DEBD43C0AE46 (XmlAttribute_t4B6CC461196FBF5CC9F777E74CC82C98E0CA9D18* __this, const RuntimeMethod* method) ;
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR bool String_op_Equality_m030E1B219352228970A076136E455C4E568C02C1 (String_t* ___0_a, String_t* ___1_b, const RuntimeMethod* method) ;
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR int32_t SmallXmlNodeList_get_Count_m79F61C89FC8ABD73BCB56938310461EFF1DF14BF (SmallXmlNodeList_tAF536F4BFCA26BF1B0E912818E921A3181D49EF0* __this, const RuntimeMethod* method) ;
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR void Object__ctor_mE837C6B9FA8C6D5D109F4B2EC885D79919AC0EA2 (RuntimeObject* __this, const RuntimeMethod* method) ;
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR bool XmlChildEnumerator_MoveNext_m038B637C00AC0482CAD4E35995F2CF2AE8E3D556 (XmlChildEnumerator_tDD03B83EAA250AB86DFF4B53C1430DA942E85939* __this, const RuntimeMethod* method) ;
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR XmlNode_t3180B9B3D5C36CD58F5327D9F13458E3B3F030AF* XmlChildEnumerator_get_Current_m149208EFC92AEDC42F82839102B3D5F017DA4C46 (XmlChildEnumerator_tDD03B83EAA250AB86DFF4B53C1430DA942E85939* __this, const RuntimeMethod* method) ;
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR void InvalidOperationException__ctor_mE4CB6F4712AB6D99A2358FBAE2E052B3EE976162 (InvalidOperationException_t5DDE4D49B7405FAAB1E4576F4715A42A3FAD4BAB* __this, String_t* ___0_message, const RuntimeMethod* method) ;
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR void XmlNodeList__ctor_m29940F85BF8D20E6FFB9732648DC6DAD10539BD3 (XmlNodeList_tFC0CEED3A006BEDF6A1A420F7CB55AC33E9B7F85* __this, const RuntimeMethod* method) ;
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR void XmlChildEnumerator__ctor_mC51E8B709CB2FCB079D44B180D5EF3DF9DBC3671 (XmlChildEnumerator_tDD03B83EAA250AB86DFF4B53C1430DA942E85939* __this, XmlNode_t3180B9B3D5C36CD58F5327D9F13458E3B3F030AF* ___0_container, const RuntimeMethod* method) ;
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR void XmlElementList__ctor_m38E1065BC153887F600A64F0FAF6D595A928BCB1 (XmlElementList_tFE0D5337668FDE708796D7FDB669AE4A290CAB57* __this, XmlNode_t3180B9B3D5C36CD58F5327D9F13458E3B3F030AF* ___0_parent, String_t* ___1_name, const RuntimeMethod* method) ;
IL2CPP_MANAGED_FORCE_INLINE IL2CPP_METHOD_ATTR XmlNameTable_tBDBAACFF3DB40A8E6AF3BDC11F0FF166CF11ABB8* XmlImplementation_get_NameTable_m8951702F59619310BD4A9528D2FF471352518CDF_inline (XmlImplementation_t4B3F467B76BD95C919C40424196C55B38EEC0F4D* __this, const RuntimeMethod* method) ;
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR Delegate_t* Delegate_Combine_m1F725AEF318BE6F0426863490691A6F4606E7D00 (Delegate_t* ___0_a, Delegate_t* ___1_b, const RuntimeMethod* method) ;
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR Delegate_t* Delegate_Remove_m8B7DD5661308FA972E23CA1CC3FC9CEB355504E3 (Delegate_t* ___0_source, Delegate_t* ___1_value, const RuntimeMethod* method) ;
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR void EmptyEnumerator__ctor_m05DA73C42354A4E549CCA7E9485B294057ABA981 (EmptyEnumerator_t84EC9187C8460EE98E675ED9258AE4DF2A6776DA* __this, const RuntimeMethod* method) ;
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR void XmlSchemaInfo__ctor_mEB56BD698D44A8AAA47668230E0CDFE8463ABD08 (XmlSchemaInfo_t6686870BCAA40A8EFD7987976F386DD40DD5323B* __this, int32_t ___0_validity, const RuntimeMethod* method) ;
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR XmlDocument_t4DE82998E642C5C21A4A620A5278237C70D3E42B* XmlNode_get_Document_mF9753FAD0D7BFDBDAE7FEA6B196D7CA2982A373C (XmlNode_t3180B9B3D5C36CD58F5327D9F13458E3B3F030AF* __this, const RuntimeMethod* method) ;
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR void XmlElementListListener__ctor_mDCC66BFA14921AA8ADAF5ED16079AB47D603B557 (XmlElementListListener_t1513DCDD9C9C5B56690342DCC4445CB4BE6E7E79* __this, XmlDocument_t4DE82998E642C5C21A4A620A5278237C70D3E42B* ___0_doc, XmlElementList_tFE0D5337668FDE708796D7FDB669AE4A290CAB57* ___1_elemList, const RuntimeMethod* method) ;
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR void WeakReference__ctor_m5F9E2F970CD85965A003C0B37ABDBFAA1F5CF241 (WeakReference_tD4B0518CE911FFD9FAAB3FCD492644A354312D8E* __this, RuntimeObject* ___0_target, const RuntimeMethod* method) ;
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR void Object_Finalize_mC98C96301CCABFE00F1A7EF8E15DF507CACD42B2 (RuntimeObject* __this, const RuntimeMethod* method) ;
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR XmlNameTable_tBDBAACFF3DB40A8E6AF3BDC11F0FF166CF11ABB8* XmlDocument_get_NameTable_m4B913865A24AEA917172F75CBDCE94C81CCB7E2C (XmlDocument_t4DE82998E642C5C21A4A620A5278237C70D3E42B* __this, const RuntimeMethod* method) ;
IL2CPP_MANAGED_FORCE_INLINE IL2CPP_METHOD_ATTR XmlNode_t3180B9B3D5C36CD58F5327D9F13458E3B3F030AF* XmlNodeChangedEventArgs_get_Node_mB5FA92B160E0200CF56DFCBAE3FC40F0D5C9B138_inline (XmlNodeChangedEventArgs_t965F2E02F8433933D197A0D1AF1EFEEDB524DF3A* __this, const RuntimeMethod* method) ;
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR bool XmlElementList_IsMatch_m9459349F92B576BEB725AA24F6337126DC1DE34B (XmlElementList_tFE0D5337668FDE708796D7FDB669AE4A290CAB57* __this, XmlNode_t3180B9B3D5C36CD58F5327D9F13458E3B3F030AF* ___0_curNode, const RuntimeMethod* method) ;
IL2CPP_MANAGED_FORCE_INLINE IL2CPP_METHOD_ATTR int32_t XmlNodeChangedEventArgs_get_Action_mD307B00A56DE5422A4EE4915400EC966DF80BD04_inline (XmlNodeChangedEventArgs_t965F2E02F8433933D197A0D1AF1EFEEDB524DF3A* __this, const RuntimeMethod* method) ;
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR void XmlElementList__ctor_mE9CE5108CF2B91213733B81C26C40C57D5726B07 (XmlElementList_tFE0D5337668FDE708796D7FDB669AE4A290CAB57* __this, XmlNode_t3180B9B3D5C36CD58F5327D9F13458E3B3F030AF* ___0_parent, const RuntimeMethod* method) ;
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR bool Ref_Equal_m60FD3EF02546279815B785F856568B01F60FE1AF (String_t* ___0_strA, String_t* ___1_strB, const RuntimeMethod* method) ;
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR XmlNode_t3180B9B3D5C36CD58F5327D9F13458E3B3F030AF* XmlElementList_NextElemInPreOrder_m6AA47AD12DFF62032C6A09B5EC1879631E0E0A6C (XmlElementList_tFE0D5337668FDE708796D7FDB669AE4A290CAB57* __this, XmlNode_t3180B9B3D5C36CD58F5327D9F13458E3B3F030AF* ___0_curNode, const RuntimeMethod* method) ;
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR XmlNode_t3180B9B3D5C36CD58F5327D9F13458E3B3F030AF* XmlElementList_PrevElemInPreOrder_m3DE0B0989925E78696BA5CAA68854052E911EE2B (XmlElementList_tFE0D5337668FDE708796D7FDB669AE4A290CAB57* __this, XmlNode_t3180B9B3D5C36CD58F5327D9F13458E3B3F030AF* ___0_curNode, const RuntimeMethod* method) ;
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR XmlNode_t3180B9B3D5C36CD58F5327D9F13458E3B3F030AF* XmlElementList_GetMatchingNode_m2CC1EB6A57B17D3B4A8FC7B16226F8ED1E3EC137 (XmlElementList_tFE0D5337668FDE708796D7FDB669AE4A290CAB57* __this, XmlNode_t3180B9B3D5C36CD58F5327D9F13458E3B3F030AF* ___0_n, bool ___1_bNext, const RuntimeMethod* method) ;
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR void XmlEmptyElementListEnumerator__ctor_m7433D2C2DDEF90DD50C7AF1816454F41949D5314 (XmlEmptyElementListEnumerator_tBA7DE21795A44BB9D31419C2CBB40A83D6DBEE84* __this, XmlElementList_tFE0D5337668FDE708796D7FDB669AE4A290CAB57* ___0_list, const RuntimeMethod* method) ;
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR void XmlElementListEnumerator__ctor_m98B779C6190C647319FDF934EEC2C9DC22B63204 (XmlElementListEnumerator_tFF3D3868A2EFB7D39A510085383C371172943794* __this, XmlElementList_tFE0D5337668FDE708796D7FDB669AE4A290CAB57* ___0_list, const RuntimeMethod* method) ;
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR void GC_SuppressFinalize_m71815DBD5A0CD2EA1BE43317B08B7A14949EDC65 (RuntimeObject* ___0_obj, const RuntimeMethod* method) ;
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR void XmlElementListListener_Unregister_m200394882802F40AE1337CE7EEC60ACAA606287C (XmlElementListListener_t1513DCDD9C9C5B56690342DCC4445CB4BE6E7E79* __this, const RuntimeMethod* method) ;
IL2CPP_MANAGED_FORCE_INLINE IL2CPP_METHOD_ATTR int32_t XmlElementList_get_ChangeCount_m6C653A302721EA1D8C41326E169F2ED54CD9EE34_inline (XmlElementList_tFE0D5337668FDE708796D7FDB669AE4A290CAB57* __this, const RuntimeMethod* method) ;
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR XmlNode_t3180B9B3D5C36CD58F5327D9F13458E3B3F030AF* XmlElementList_GetNextNode_m54C6F8839217646D19DF43C2AB3FFE87ECABF04B (XmlElementList_tFE0D5337668FDE708796D7FDB669AE4A290CAB57* __this, XmlNode_t3180B9B3D5C36CD58F5327D9F13458E3B3F030AF* ___0_n, const RuntimeMethod* method) ;
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR void XmlNodeChangedEventHandler__ctor_mD51AE5833F8A9B1EA9DB8DC54FEF3AD64763AED9 (XmlNodeChangedEventHandler_t907F6E9CB8DE238635028EC468AD41CAB0BA269B* __this, RuntimeObject* ___0_object, intptr_t ___1_method, const RuntimeMethod* method) ;
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR void XmlDocument_add_NodeInserted_m5B03345EAC48958B104BA2033818370BA6E2243D (XmlDocument_t4DE82998E642C5C21A4A620A5278237C70D3E42B* __this, XmlNodeChangedEventHandler_t907F6E9CB8DE238635028EC468AD41CAB0BA269B* ___0_value, const RuntimeMethod* method) ;
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR void XmlDocument_add_NodeRemoved_m30754B20BE8A674BD5FDECF175E2419DD2AE3E1C (XmlDocument_t4DE82998E642C5C21A4A620A5278237C70D3E42B* __this, XmlNodeChangedEventHandler_t907F6E9CB8DE238635028EC468AD41CAB0BA269B* ___0_value, const RuntimeMethod* method) ;
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR void Monitor_Exit_m05B2CF037E2214B3208198C282490A2A475653FA (RuntimeObject* ___0_obj, const RuntimeMethod* method) ;
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR void Monitor_Enter_m3CDB589DA1300B513D55FDCFB52B63E879794149 (RuntimeObject* ___0_obj, bool* ___1_lockTaken, const RuntimeMethod* method) ;
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR void XmlElementList_ConcurrencyCheck_mFC4A7DF2FC6B07D06430F714671AB3468E068386 (XmlElementList_tFE0D5337668FDE708796D7FDB669AE4A290CAB57* __this, XmlNodeChangedEventArgs_t965F2E02F8433933D197A0D1AF1EFEEDB524DF3A* ___0_args, const RuntimeMethod* method) ;
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR void XmlDocument_remove_NodeInserted_mF46D0B84DB934188F551BCC766080CDA9A787616 (XmlDocument_t4DE82998E642C5C21A4A620A5278237C70D3E42B* __this, XmlNodeChangedEventHandler_t907F6E9CB8DE238635028EC468AD41CAB0BA269B* ___0_value, const RuntimeMethod* method) ;
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR void XmlDocument_remove_NodeRemoved_m33B3778BE1EB94A010FDE8F860641821A7A079A3 (XmlDocument_t4DE82998E642C5C21A4A620A5278237C70D3E42B* __this, XmlNodeChangedEventHandler_t907F6E9CB8DE238635028EC468AD41CAB0BA269B* ___0_value, const RuntimeMethod* method) ;
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR Il2CppChar String_get_Chars_mC49DF0CD2D3BE7BE97B3AD9C995BE3094F8E36D3 (String_t* __this, int32_t ___0_index, const RuntimeMethod* method) ;
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR void ArgumentOutOfRangeException__ctor_mBC1D5DEEA1BA41DE77228CB27D6BAFEB6DCCBF4A (ArgumentOutOfRangeException_tEA2822DAF62B10EEED00E0E3A341D4BAF78CF85F* __this, String_t* ___0_paramName, const RuntimeMethod* method) ;
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR void XmlChildNodes__ctor_mE297CC7E680D376A26339D6067C7E434271234B5 (XmlChildNodes_tBD3126123F4810D8B272B8201C07FE70CDC9C5E2* __this, XmlNode_t3180B9B3D5C36CD58F5327D9F13458E3B3F030AF* ___0_container, const RuntimeMethod* method) ;
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR StringBuilder_t* StringBuilder_Append_m08904D74E0C78E5F36DCD9C9303BDD07886D9F7D (StringBuilder_t* __this, String_t* ___0_value, const RuntimeMethod* method) ;
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR void XmlNode_AppendChildText_m8C1493DF2F031942BD58F96DE8EFC365194561A1 (XmlNode_t3180B9B3D5C36CD58F5327D9F13458E3B3F030AF* __this, StringBuilder_t* ___0_builder, const RuntimeMethod* method) ;
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR void StringBuilder__ctor_m1D99713357DE05DAFA296633639DB55F8C30587D (StringBuilder_t* __this, const RuntimeMethod* method) ;
IL2CPP_MANAGED_FORCE_INLINE IL2CPP_METHOD_ATTR void DebuggerDisplayXmlNodeProxy__ctor_mB9B6D906F2EBADBBDBA79FE184D6860363C32356_inline (DebuggerDisplayXmlNodeProxy_t75382D048931DB9A7D8EFB35A56EEC1D8241FB07* __this, XmlNode_t3180B9B3D5C36CD58F5327D9F13458E3B3F030AF* ___0_node, const RuntimeMethod* method) ;
IL2CPP_MANAGED_FORCE_INLINE IL2CPP_METHOD_ATTR String_t* XmlDocumentType_get_SystemId_m17B225C509C0F5A4ECBB838AA81D15DED4016909_inline (XmlDocumentType_tFD7379E45412FC2EBD65280ED3DE7A1C941A8D9E* __this, const RuntimeMethod* method) ;
IL2CPP_MANAGED_FORCE_INLINE IL2CPP_METHOD_ATTR String_t* XmlDocumentType_get_PublicId_m9E81FAF2D0AA9DC847D118813CBEB085F0CE1955_inline (XmlDocumentType_tFD7379E45412FC2EBD65280ED3DE7A1C941A8D9E* __this, const RuntimeMethod* method) ;
IL2CPP_MANAGED_FORCE_INLINE IL2CPP_METHOD_ATTR String_t* XmlDocumentType_get_InternalSubset_mBD8D585ED0401EE1FC02F6FD5445E2109B083FEF_inline (XmlDocumentType_tFD7379E45412FC2EBD65280ED3DE7A1C941A8D9E* __this, const RuntimeMethod* method) ;
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR String_t* DebuggerDisplayXmlNodeProxy_ToString_mAE6AF3B460B62D08C1090FA5FA67BB80EB091D48 (DebuggerDisplayXmlNodeProxy_t75382D048931DB9A7D8EFB35A56EEC1D8241FB07* __this, const RuntimeMethod* method) ;
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR RuntimeObject* XmlCharType_get_StaticLock_m39193568D86AD7E7A5AA4B3745D64C6F5D60472E (const RuntimeMethod* method) ;
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR void XmlCharType_SetProperties_m8964034908DAEC93686ADC1D879E15967B91D4D4 (ByteU5BU5D_tA6237BF417AE52AD70CFB4EF24A7A82613DF9031* ___0_chProps, String_t* ___1_ranges, uint8_t ___2_value, const RuntimeMethod* method) ;
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR void Thread_MemoryBarrier_m83873F1E6CEB16C0781941141382DA874A36097D (const RuntimeMethod* method) ;
IL2CPP_MANAGED_FORCE_INLINE IL2CPP_METHOD_ATTR void XmlCharType__ctor_mB4A379AC51751894C31365E176E3B9BFB5ABC771_inline (XmlCharType_t7C471894C8862126737E800F5A14AACBD14FCBC3* __this, ByteU5BU5D_tA6237BF417AE52AD70CFB4EF24A7A82613DF9031* ___0_charProperties, const RuntimeMethod* method) ;
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR void XmlCharType_InitInstance_m486814EDE27EF404061078D3EA69301B23F01378 (const RuntimeMethod* method) ;
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR void StringBuilder__ctor_m2619CA8D2C3476DF1A302D9D941498BB1C6164C5 (StringBuilder_t* __this, int32_t ___0_capacity, const RuntimeMethod* method) ;
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR StringBuilder_t* StringBuilder_Append_mE7CA22532FEF1545B8BC357CB7CAA304BA264703 (StringBuilder_t* __this, String_t* ___0_value, int32_t ___1_startIndex, int32_t ___2_count, const RuntimeMethod* method) ;
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR StringBuilder_t* StringBuilder_Append_m71228B30F05724CD2CD96D9611DCD61BFB96A6E1 (StringBuilder_t* __this, Il2CppChar ___0_value, const RuntimeMethod* method) ;
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR XmlCharType_t7C471894C8862126737E800F5A14AACBD14FCBC3 XmlCharType_get_Instance_m1279A3FC91B74599A9BC76979E50DBFC07F67C55 (const RuntimeMethod* method) ;
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR void RuntimeHelpers_InitializeArray_m751372AA3F24FBF6DA9B9D687CBFA2DE436CAB9B (RuntimeArray* ___0_array, RuntimeFieldHandle_t6E4C45B6D2EA12FC99185805A7E77527899B25C5 ___1_fldHandle, const RuntimeMethod* method) ;
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR void XmlSchemaObject__ctor_m72D9D6DC7E5B4FBB6ADEB97F688474AF8AC7CADE (XmlSchemaObject_t82CCD87AADB4BEF4E9DFE4C501F09EDA372AA19D* __this, const RuntimeMethod* method) ;
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR void XmlSchemaInfo_Clear_m6EACF6A1BB70B47CBC2865B8CCC74BEA67F96FFE (XmlSchemaInfo_t6686870BCAA40A8EFD7987976F386DD40DD5323B* __this, const RuntimeMethod* method) ;
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR void XmlSchemaInfo__ctor_mEEC95F1F490C5D62BF1CC6DB16FE4938E5C1C341 (XmlSchemaInfo_t6686870BCAA40A8EFD7987976F386DD40DD5323B* __this, const RuntimeMethod* method) ;
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR void XmlSchemaAnnotated__ctor_m094C7E3CD496FCF801D2A9A68E6D410B634096BC (XmlSchemaAnnotated_t41DBE9DF8776C6C639B33A712F25497C672B4E04* __this, const RuntimeMethod* method) ;
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR void EmptyParticle__ctor_m7FE5C70CB31BBBE2FAF38D403AC196F379DAC079 (EmptyParticle_tB70737785BCC28912378BA174C18956FF658454A* __this, const RuntimeMethod* method) ;
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR void XmlSchemaParticle__ctor_mDF61E3EE9B7D4102592DA20D02619EBF4D04F9A1 (XmlSchemaParticle_t9A7544A3F66C6E4B8900456CF705A6F9D95655EE* __this, const RuntimeMethod* method) ;
#ifdef __clang__
#pragma clang diagnostic push
#pragma clang diagnostic ignored "-Winvalid-offsetof"
#pragma clang diagnostic ignored "-Wunused-variable"
#endif
#ifdef __clang__
#pragma clang diagnostic pop
#endif
#ifdef __clang__
#pragma clang diagnostic push
#pragma clang diagnostic ignored "-Winvalid-offsetof"
#pragma clang diagnostic ignored "-Wunused-variable"
#endif
// Method Definition Index: 93859
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR String_t* XmlReader_get_Name_m584CCDFC01FC6C2679778096652800364589F784 (XmlReader_t4C709DEF5F01606ECB60B638F1BD6F6E0A9116FD* __this, const RuntimeMethod* method) 
{
	static bool s_Il2CppMethodInitialized;
	if (!s_Il2CppMethodInitialized)
	{
		il2cpp_codegen_initialize_runtime_metadata((uintptr_t*)&_stringLiteral876C4B39B6E4D0187090400768899C71D99DE90D);
		s_Il2CppMethodInitialized = true;
	}
	{
		String_t* L_0;
		L_0 = VirtualFuncInvoker0< String_t* >::Invoke(7, __this);
		NullCheck(L_0);
		int32_t L_1;
		L_1 = String_get_Length_m42625D67623FA5CC7A44D47425CE86FB946542D2_inline(L_0, NULL);
		if (L_1)
		{
			goto IL_0014;
		}
	}
	{
		String_t* L_2;
		L_2 = VirtualFuncInvoker0< String_t* >::Invoke(6, __this);
		return L_2;
	}

IL_0014:
	{
		XmlNameTable_tBDBAACFF3DB40A8E6AF3BDC11F0FF166CF11ABB8* L_3;
		L_3 = VirtualFuncInvoker0< XmlNameTable_tBDBAACFF3DB40A8E6AF3BDC11F0FF166CF11ABB8* >::Invoke(10, __this);
		String_t* L_4;
		L_4 = VirtualFuncInvoker0< String_t* >::Invoke(7, __this);
		String_t* L_5;
		L_5 = VirtualFuncInvoker0< String_t* >::Invoke(6, __this);
		String_t* L_6;
		L_6 = String_Concat_m8855A6DE10F84DA7F4EC113CADDB59873A25573B(L_4, _stringLiteral876C4B39B6E4D0187090400768899C71D99DE90D, L_5, NULL);
		NullCheck(L_3);
		String_t* L_7;
		L_7 = VirtualFuncInvoker1< String_t*, String_t* >::Invoke(4, L_3, L_6);
		return L_7;
	}
}
// Method Definition Index: 93865
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR RuntimeObject* XmlReader_get_debuggerDisplayProxy_mB583F6633EE6F7D376F3DA6298560FF9AC1E9491 (XmlReader_t4C709DEF5F01606ECB60B638F1BD6F6E0A9116FD* __this, const RuntimeMethod* method) 
{
	static bool s_Il2CppMethodInitialized;
	if (!s_Il2CppMethodInitialized)
	{
		il2cpp_codegen_initialize_runtime_metadata((uintptr_t*)&XmlReaderDebuggerDisplayProxy_tCBCA1821E9EDC752D1884A7A4343C3B0C7134AC9_il2cpp_TypeInfo_var);
		s_Il2CppMethodInitialized = true;
	}
	{
		XmlReaderDebuggerDisplayProxy_tCBCA1821E9EDC752D1884A7A4343C3B0C7134AC9 L_0;
		memset((&L_0), 0, sizeof(L_0));
		XmlReaderDebuggerDisplayProxy__ctor_m56B82747E748FBB7BC8D0A1728D526EA23B54C1A_inline((&L_0), __this, NULL);
		XmlReaderDebuggerDisplayProxy_tCBCA1821E9EDC752D1884A7A4343C3B0C7134AC9 L_1 = L_0;
		RuntimeObject* L_2 = Box(XmlReaderDebuggerDisplayProxy_tCBCA1821E9EDC752D1884A7A4343C3B0C7134AC9_il2cpp_TypeInfo_var, &L_1);
		return L_2;
	}
}
// Method Definition Index: 93866
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR void XmlReader__cctor_m9FF3BD38D3644E099B8305E251679A77A0DF493E (const RuntimeMethod* method) 
{
	static bool s_Il2CppMethodInitialized;
	if (!s_Il2CppMethodInitialized)
	{
		il2cpp_codegen_initialize_runtime_metadata((uintptr_t*)&XmlReader_t4C709DEF5F01606ECB60B638F1BD6F6E0A9116FD_il2cpp_TypeInfo_var);
		s_Il2CppMethodInitialized = true;
	}
	{
		((XmlReader_t4C709DEF5F01606ECB60B638F1BD6F6E0A9116FD_StaticFields*)il2cpp_codegen_static_fields_for(XmlReader_t4C709DEF5F01606ECB60B638F1BD6F6E0A9116FD_il2cpp_TypeInfo_var))->___IsTextualNodeBitmap = ((int32_t)24600);
		((XmlReader_t4C709DEF5F01606ECB60B638F1BD6F6E0A9116FD_StaticFields*)il2cpp_codegen_static_fields_for(XmlReader_t4C709DEF5F01606ECB60B638F1BD6F6E0A9116FD_il2cpp_TypeInfo_var))->___CanReadContentAsBitmap = ((int32_t)123324);
		((XmlReader_t4C709DEF5F01606ECB60B638F1BD6F6E0A9116FD_StaticFields*)il2cpp_codegen_static_fields_for(XmlReader_t4C709DEF5F01606ECB60B638F1BD6F6E0A9116FD_il2cpp_TypeInfo_var))->___HasValueBitmap = ((int32_t)157084);
		return;
	}
}
#ifdef __clang__
#pragma clang diagnostic pop
#endif
#ifdef __clang__
#pragma clang diagnostic push
#pragma clang diagnostic ignored "-Winvalid-offsetof"
#pragma clang diagnostic ignored "-Wunused-variable"
#endif
IL2CPP_EXTERN_C void XmlReaderDebuggerDisplayProxy_tCBCA1821E9EDC752D1884A7A4343C3B0C7134AC9_marshal_pinvoke(const XmlReaderDebuggerDisplayProxy_tCBCA1821E9EDC752D1884A7A4343C3B0C7134AC9& unmarshaled, XmlReaderDebuggerDisplayProxy_tCBCA1821E9EDC752D1884A7A4343C3B0C7134AC9_marshaled_pinvoke& marshaled)
{
	static bool s_Il2CppMethodInitialized;
	if (!s_Il2CppMethodInitialized)
	{
		il2cpp_codegen_initialize_runtime_metadata((uintptr_t*)&XmlReaderDebuggerDisplayProxy_tCBCA1821E9EDC752D1884A7A4343C3B0C7134AC9_0_0_0_var);
		il2cpp_codegen_initialize_runtime_metadata((uintptr_t*)&XmlReaderDebuggerDisplayProxy_tCBCA1821E9EDC752D1884A7A4343C3B0C7134AC9____reader_FieldInfo_var);
		s_Il2CppMethodInitialized = true;
	}
	Exception_t* ___readerException = il2cpp_codegen_get_marshal_directive_exception("Cannot marshal field '%s' of type '%s': Reference type field marshaling is not supported.", XmlReaderDebuggerDisplayProxy_tCBCA1821E9EDC752D1884A7A4343C3B0C7134AC9____reader_FieldInfo_var, XmlReaderDebuggerDisplayProxy_tCBCA1821E9EDC752D1884A7A4343C3B0C7134AC9_0_0_0_var);
	IL2CPP_RAISE_MANAGED_EXCEPTION(___readerException, NULL);
}
IL2CPP_EXTERN_C void XmlReaderDebuggerDisplayProxy_tCBCA1821E9EDC752D1884A7A4343C3B0C7134AC9_marshal_pinvoke_back(const XmlReaderDebuggerDisplayProxy_tCBCA1821E9EDC752D1884A7A4343C3B0C7134AC9_marshaled_pinvoke& marshaled, XmlReaderDebuggerDisplayProxy_tCBCA1821E9EDC752D1884A7A4343C3B0C7134AC9& unmarshaled)
{
	static bool s_Il2CppMethodInitialized;
	if (!s_Il2CppMethodInitialized)
	{
		il2cpp_codegen_initialize_runtime_metadata((uintptr_t*)&XmlReaderDebuggerDisplayProxy_tCBCA1821E9EDC752D1884A7A4343C3B0C7134AC9_0_0_0_var);
		il2cpp_codegen_initialize_runtime_metadata((uintptr_t*)&XmlReaderDebuggerDisplayProxy_tCBCA1821E9EDC752D1884A7A4343C3B0C7134AC9____reader_FieldInfo_var);
		s_Il2CppMethodInitialized = true;
	}
	Exception_t* ___readerException = il2cpp_codegen_get_marshal_directive_exception("Cannot marshal field '%s' of type '%s': Reference type field marshaling is not supported.", XmlReaderDebuggerDisplayProxy_tCBCA1821E9EDC752D1884A7A4343C3B0C7134AC9____reader_FieldInfo_var, XmlReaderDebuggerDisplayProxy_tCBCA1821E9EDC752D1884A7A4343C3B0C7134AC9_0_0_0_var);
	IL2CPP_RAISE_MANAGED_EXCEPTION(___readerException, NULL);
}
IL2CPP_EXTERN_C void XmlReaderDebuggerDisplayProxy_tCBCA1821E9EDC752D1884A7A4343C3B0C7134AC9_marshal_pinvoke_cleanup(XmlReaderDebuggerDisplayProxy_tCBCA1821E9EDC752D1884A7A4343C3B0C7134AC9_marshaled_pinvoke& marshaled)
{
}
IL2CPP_EXTERN_C void XmlReaderDebuggerDisplayProxy_tCBCA1821E9EDC752D1884A7A4343C3B0C7134AC9_marshal_com(const XmlReaderDebuggerDisplayProxy_tCBCA1821E9EDC752D1884A7A4343C3B0C7134AC9& unmarshaled, XmlReaderDebuggerDisplayProxy_tCBCA1821E9EDC752D1884A7A4343C3B0C7134AC9_marshaled_com& marshaled)
{
	static bool s_Il2CppMethodInitialized;
	if (!s_Il2CppMethodInitialized)
	{
		il2cpp_codegen_initialize_runtime_metadata((uintptr_t*)&XmlReaderDebuggerDisplayProxy_tCBCA1821E9EDC752D1884A7A4343C3B0C7134AC9_0_0_0_var);
		il2cpp_codegen_initialize_runtime_metadata((uintptr_t*)&XmlReaderDebuggerDisplayProxy_tCBCA1821E9EDC752D1884A7A4343C3B0C7134AC9____reader_FieldInfo_var);
		s_Il2CppMethodInitialized = true;
	}
	Exception_t* ___readerException = il2cpp_codegen_get_marshal_directive_exception("Cannot marshal field '%s' of type '%s': Reference type field marshaling is not supported.", XmlReaderDebuggerDisplayProxy_tCBCA1821E9EDC752D1884A7A4343C3B0C7134AC9____reader_FieldInfo_var, XmlReaderDebuggerDisplayProxy_tCBCA1821E9EDC752D1884A7A4343C3B0C7134AC9_0_0_0_var);
	IL2CPP_RAISE_MANAGED_EXCEPTION(___readerException, NULL);
}
IL2CPP_EXTERN_C void XmlReaderDebuggerDisplayProxy_tCBCA1821E9EDC752D1884A7A4343C3B0C7134AC9_marshal_com_back(const XmlReaderDebuggerDisplayProxy_tCBCA1821E9EDC752D1884A7A4343C3B0C7134AC9_marshaled_com& marshaled, XmlReaderDebuggerDisplayProxy_tCBCA1821E9EDC752D1884A7A4343C3B0C7134AC9& unmarshaled)
{
	static bool s_Il2CppMethodInitialized;
	if (!s_Il2CppMethodInitialized)
	{
		il2cpp_codegen_initialize_runtime_metadata((uintptr_t*)&XmlReaderDebuggerDisplayProxy_tCBCA1821E9EDC752D1884A7A4343C3B0C7134AC9_0_0_0_var);
		il2cpp_codegen_initialize_runtime_metadata((uintptr_t*)&XmlReaderDebuggerDisplayProxy_tCBCA1821E9EDC752D1884A7A4343C3B0C7134AC9____reader_FieldInfo_var);
		s_Il2CppMethodInitialized = true;
	}
	Exception_t* ___readerException = il2cpp_codegen_get_marshal_directive_exception("Cannot marshal field '%s' of type '%s': Reference type field marshaling is not supported.", XmlReaderDebuggerDisplayProxy_tCBCA1821E9EDC752D1884A7A4343C3B0C7134AC9____reader_FieldInfo_var, XmlReaderDebuggerDisplayProxy_tCBCA1821E9EDC752D1884A7A4343C3B0C7134AC9_0_0_0_var);
	IL2CPP_RAISE_MANAGED_EXCEPTION(___readerException, NULL);
}
IL2CPP_EXTERN_C void XmlReaderDebuggerDisplayProxy_tCBCA1821E9EDC752D1884A7A4343C3B0C7134AC9_marshal_com_cleanup(XmlReaderDebuggerDisplayProxy_tCBCA1821E9EDC752D1884A7A4343C3B0C7134AC9_marshaled_com& marshaled)
{
}
// Method Definition Index: 93867
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR void XmlReaderDebuggerDisplayProxy__ctor_m56B82747E748FBB7BC8D0A1728D526EA23B54C1A (XmlReaderDebuggerDisplayProxy_tCBCA1821E9EDC752D1884A7A4343C3B0C7134AC9* __this, XmlReader_t4C709DEF5F01606ECB60B638F1BD6F6E0A9116FD* ___0_reader, const RuntimeMethod* method) 
{
	{
		XmlReader_t4C709DEF5F01606ECB60B638F1BD6F6E0A9116FD* L_0 = ___0_reader;
		__this->___reader = L_0;
		Il2CppCodeGenWriteBarrier((void**)(&__this->___reader), (void*)L_0);
		return;
	}
}
IL2CPP_EXTERN_C  void XmlReaderDebuggerDisplayProxy__ctor_m56B82747E748FBB7BC8D0A1728D526EA23B54C1A_AdjustorThunk (RuntimeObject* __this, XmlReader_t4C709DEF5F01606ECB60B638F1BD6F6E0A9116FD* ___0_reader, const RuntimeMethod* method)
{
	XmlReaderDebuggerDisplayProxy_tCBCA1821E9EDC752D1884A7A4343C3B0C7134AC9* _thisAdjusted;
	int32_t _offset = 1;
	_thisAdjusted = reinterpret_cast<XmlReaderDebuggerDisplayProxy_tCBCA1821E9EDC752D1884A7A4343C3B0C7134AC9*>(__this + _offset);
	XmlReaderDebuggerDisplayProxy__ctor_m56B82747E748FBB7BC8D0A1728D526EA23B54C1A_inline(_thisAdjusted, ___0_reader, method);
}
// Method Definition Index: 93868
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR String_t* XmlReaderDebuggerDisplayProxy_ToString_mEBDAEFA455D93E9934990687C8ED5421E6537FFB (XmlReaderDebuggerDisplayProxy_tCBCA1821E9EDC752D1884A7A4343C3B0C7134AC9* __this, const RuntimeMethod* method) 
{
	static bool s_Il2CppMethodInitialized;
	if (!s_Il2CppMethodInitialized)
	{
		il2cpp_codegen_initialize_runtime_metadata((uintptr_t*)&StringU5BU5D_t7674CD946EC0CE7B3AE0BE70E6EE85F2ECD9F248_il2cpp_TypeInfo_var);
		il2cpp_codegen_initialize_runtime_metadata((uintptr_t*)&XmlConvert_t5D100AFD62FF717BD4F8A6CABA9EDF7A5DF719EA_il2cpp_TypeInfo_var);
		il2cpp_codegen_initialize_runtime_metadata((uintptr_t*)&XmlNodeType_tCA25567E42A464038FDA6BA318EC0A9627B4CB95_il2cpp_TypeInfo_var);
		il2cpp_codegen_initialize_runtime_metadata((uintptr_t*)&_stringLiteral1F1BC4D23D8E5661364EBEC728BE303959F6CF91);
		il2cpp_codegen_initialize_runtime_metadata((uintptr_t*)&_stringLiteral2C444E82A2EF8AEC533AC89C8394A988DC706B88);
		il2cpp_codegen_initialize_runtime_metadata((uintptr_t*)&_stringLiteral3EF4F70676F5F18B8797A6624726002F607BB8DE);
		il2cpp_codegen_initialize_runtime_metadata((uintptr_t*)&_stringLiteral42646B33B50B6AA15E22733C8900716F0FE19E1D);
		il2cpp_codegen_initialize_runtime_metadata((uintptr_t*)&_stringLiteral909AA3EFEECF6508CFB6F17EDD057508FF1D7D17);
		il2cpp_codegen_initialize_runtime_metadata((uintptr_t*)&_stringLiteral99EFCE42C9A728EA7C113BD7034366462E5E09E6);
		il2cpp_codegen_initialize_runtime_metadata((uintptr_t*)&_stringLiteral9FD9B00B05227B64D8B46A29B6D1B4FEA2C197EF);
		il2cpp_codegen_initialize_runtime_metadata((uintptr_t*)&_stringLiteralC62C64F00567C5368CAE37F4E64E1E82FF785677);
		il2cpp_codegen_initialize_runtime_metadata((uintptr_t*)&_stringLiteralD4DAA1A26807E1A567D68B161E6A95701A3FD560);
		s_Il2CppMethodInitialized = true;
	}
	int32_t V_0 = 0;
	String_t* V_1 = NULL;
	{
		XmlReader_t4C709DEF5F01606ECB60B638F1BD6F6E0A9116FD* L_0 = __this->___reader;
		NullCheck(L_0);
		int32_t L_1;
		L_1 = VirtualFuncInvoker0< int32_t >::Invoke(4, L_0);
		V_0 = L_1;
		Il2CppFakeBox<int32_t> L_2(XmlNodeType_tCA25567E42A464038FDA6BA318EC0A9627B4CB95_il2cpp_TypeInfo_var, (&V_0));
		String_t* L_3;
		L_3 = Enum_ToString_m946B0B83C4470457D0FF555D862022C72BB55741((Enum_t2A1A94B24E3B776EEF4E5E485E290BB9D4D072E2*)(&L_2), NULL);
		V_1 = L_3;
		int32_t L_4 = V_0;
		switch (((int32_t)il2cpp_codegen_subtract((int32_t)L_4, 1)))
		{
			case 0:
			{
				goto IL_006b;
			}
			case 1:
			{
				goto IL_008c;
			}
			case 2:
			{
				goto IL_00da;
			}
			case 3:
			{
				goto IL_00da;
			}
			case 4:
			{
				goto IL_006b;
			}
			case 5:
			{
				goto IL_017c;
			}
			case 6:
			{
				goto IL_008c;
			}
			case 7:
			{
				goto IL_00da;
			}
			case 8:
			{
				goto IL_017c;
			}
			case 9:
			{
				goto IL_00fd;
			}
			case 10:
			{
				goto IL_017c;
			}
			case 11:
			{
				goto IL_017c;
			}
			case 12:
			{
				goto IL_00da;
			}
			case 13:
			{
				goto IL_00da;
			}
			case 14:
			{
				goto IL_006b;
			}
			case 15:
			{
				goto IL_006b;
			}
			case 16:
			{
				goto IL_00da;
			}
		}
	}
	{
		goto IL_017c;
	}

IL_006b:
	{
		String_t* L_5 = V_1;
		XmlReader_t4C709DEF5F01606ECB60B638F1BD6F6E0A9116FD* L_6 = __this->___reader;
		NullCheck(L_6);
		String_t* L_7;
		L_7 = VirtualFuncInvoker0< String_t* >::Invoke(5, L_6);
		String_t* L_8;
		L_8 = String_Concat_m093934F71A9B351911EE46311674ED463B180006(L_5, _stringLiteral99EFCE42C9A728EA7C113BD7034366462E5E09E6, L_7, _stringLiteralC62C64F00567C5368CAE37F4E64E1E82FF785677, NULL);
		V_1 = L_8;
		goto IL_017c;
	}

IL_008c:
	{
		StringU5BU5D_t7674CD946EC0CE7B3AE0BE70E6EE85F2ECD9F248* L_9 = (StringU5BU5D_t7674CD946EC0CE7B3AE0BE70E6EE85F2ECD9F248*)(StringU5BU5D_t7674CD946EC0CE7B3AE0BE70E6EE85F2ECD9F248*)SZArrayNew(StringU5BU5D_t7674CD946EC0CE7B3AE0BE70E6EE85F2ECD9F248_il2cpp_TypeInfo_var, (uint32_t)6);
		StringU5BU5D_t7674CD946EC0CE7B3AE0BE70E6EE85F2ECD9F248* L_10 = L_9;
		String_t* L_11 = V_1;
		NullCheck(L_10);
		(L_10)->SetAt(static_cast<il2cpp_array_size_t>(0), (String_t*)L_11);
		StringU5BU5D_t7674CD946EC0CE7B3AE0BE70E6EE85F2ECD9F248* L_12 = L_10;
		NullCheck(L_12);
		(L_12)->SetAt(static_cast<il2cpp_array_size_t>(1), (String_t*)_stringLiteral99EFCE42C9A728EA7C113BD7034366462E5E09E6);
		StringU5BU5D_t7674CD946EC0CE7B3AE0BE70E6EE85F2ECD9F248* L_13 = L_12;
		XmlReader_t4C709DEF5F01606ECB60B638F1BD6F6E0A9116FD* L_14 = __this->___reader;
		NullCheck(L_14);
		String_t* L_15;
		L_15 = VirtualFuncInvoker0< String_t* >::Invoke(5, L_14);
		NullCheck(L_13);
		(L_13)->SetAt(static_cast<il2cpp_array_size_t>(2), (String_t*)L_15);
		StringU5BU5D_t7674CD946EC0CE7B3AE0BE70E6EE85F2ECD9F248* L_16 = L_13;
		NullCheck(L_16);
		(L_16)->SetAt(static_cast<il2cpp_array_size_t>(3), (String_t*)_stringLiteral3EF4F70676F5F18B8797A6624726002F607BB8DE);
		StringU5BU5D_t7674CD946EC0CE7B3AE0BE70E6EE85F2ECD9F248* L_17 = L_16;
		XmlReader_t4C709DEF5F01606ECB60B638F1BD6F6E0A9116FD* L_18 = __this->___reader;
		NullCheck(L_18);
		String_t* L_19;
		L_19 = VirtualFuncInvoker0< String_t* >::Invoke(8, L_18);
		il2cpp_codegen_runtime_class_init_inline(XmlConvert_t5D100AFD62FF717BD4F8A6CABA9EDF7A5DF719EA_il2cpp_TypeInfo_var);
		String_t* L_20;
		L_20 = XmlConvert_EscapeValueForDebuggerDisplay_m4290160B46B7BA2841826618DBBD1403371CA202(L_19, NULL);
		NullCheck(L_17);
		(L_17)->SetAt(static_cast<il2cpp_array_size_t>(4), (String_t*)L_20);
		StringU5BU5D_t7674CD946EC0CE7B3AE0BE70E6EE85F2ECD9F248* L_21 = L_17;
		NullCheck(L_21);
		(L_21)->SetAt(static_cast<il2cpp_array_size_t>(5), (String_t*)_stringLiteralC62C64F00567C5368CAE37F4E64E1E82FF785677);
		String_t* L_22;
		L_22 = String_Concat_m647EBF831F54B6DF7D5AFA5FD012CF4EE7571B6A(L_21, NULL);
		V_1 = L_22;
		goto IL_017c;
	}

IL_00da:
	{
		String_t* L_23 = V_1;
		XmlReader_t4C709DEF5F01606ECB60B638F1BD6F6E0A9116FD* L_24 = __this->___reader;
		NullCheck(L_24);
		String_t* L_25;
		L_25 = VirtualFuncInvoker0< String_t* >::Invoke(8, L_24);
		il2cpp_codegen_runtime_class_init_inline(XmlConvert_t5D100AFD62FF717BD4F8A6CABA9EDF7A5DF719EA_il2cpp_TypeInfo_var);
		String_t* L_26;
		L_26 = XmlConvert_EscapeValueForDebuggerDisplay_m4290160B46B7BA2841826618DBBD1403371CA202(L_25, NULL);
		String_t* L_27;
		L_27 = String_Concat_m093934F71A9B351911EE46311674ED463B180006(L_23, _stringLiteral2C444E82A2EF8AEC533AC89C8394A988DC706B88, L_26, _stringLiteralC62C64F00567C5368CAE37F4E64E1E82FF785677, NULL);
		V_1 = L_27;
		goto IL_017c;
	}

IL_00fd:
	{
		String_t* L_28 = V_1;
		XmlReader_t4C709DEF5F01606ECB60B638F1BD6F6E0A9116FD* L_29 = __this->___reader;
		NullCheck(L_29);
		String_t* L_30;
		L_30 = VirtualFuncInvoker0< String_t* >::Invoke(5, L_29);
		String_t* L_31;
		L_31 = String_Concat_m093934F71A9B351911EE46311674ED463B180006(L_28, _stringLiteral99EFCE42C9A728EA7C113BD7034366462E5E09E6, L_30, _stringLiteral42646B33B50B6AA15E22733C8900716F0FE19E1D, NULL);
		V_1 = L_31;
		String_t* L_32 = V_1;
		XmlReader_t4C709DEF5F01606ECB60B638F1BD6F6E0A9116FD* L_33 = __this->___reader;
		NullCheck(L_33);
		String_t* L_34;
		L_34 = VirtualFuncInvoker1< String_t*, String_t* >::Invoke(9, L_33, _stringLiteralD4DAA1A26807E1A567D68B161E6A95701A3FD560);
		String_t* L_35;
		L_35 = String_Concat_m093934F71A9B351911EE46311674ED463B180006(L_32, _stringLiteral9FD9B00B05227B64D8B46A29B6D1B4FEA2C197EF, L_34, _stringLiteralC62C64F00567C5368CAE37F4E64E1E82FF785677, NULL);
		V_1 = L_35;
		String_t* L_36 = V_1;
		XmlReader_t4C709DEF5F01606ECB60B638F1BD6F6E0A9116FD* L_37 = __this->___reader;
		NullCheck(L_37);
		String_t* L_38;
		L_38 = VirtualFuncInvoker1< String_t*, String_t* >::Invoke(9, L_37, _stringLiteral1F1BC4D23D8E5661364EBEC728BE303959F6CF91);
		String_t* L_39;
		L_39 = String_Concat_m093934F71A9B351911EE46311674ED463B180006(L_36, _stringLiteral909AA3EFEECF6508CFB6F17EDD057508FF1D7D17, L_38, _stringLiteralC62C64F00567C5368CAE37F4E64E1E82FF785677, NULL);
		V_1 = L_39;
		String_t* L_40 = V_1;
		XmlReader_t4C709DEF5F01606ECB60B638F1BD6F6E0A9116FD* L_41 = __this->___reader;
		NullCheck(L_41);
		String_t* L_42;
		L_42 = VirtualFuncInvoker0< String_t* >::Invoke(8, L_41);
		il2cpp_codegen_runtime_class_init_inline(XmlConvert_t5D100AFD62FF717BD4F8A6CABA9EDF7A5DF719EA_il2cpp_TypeInfo_var);
		String_t* L_43;
		L_43 = XmlConvert_EscapeValueForDebuggerDisplay_m4290160B46B7BA2841826618DBBD1403371CA202(L_42, NULL);
		String_t* L_44;
		L_44 = String_Concat_m093934F71A9B351911EE46311674ED463B180006(L_40, _stringLiteral2C444E82A2EF8AEC533AC89C8394A988DC706B88, L_43, _stringLiteralC62C64F00567C5368CAE37F4E64E1E82FF785677, NULL);
		V_1 = L_44;
	}

IL_017c:
	{
		String_t* L_45 = V_1;
		return L_45;
	}
}
IL2CPP_EXTERN_C  String_t* XmlReaderDebuggerDisplayProxy_ToString_mEBDAEFA455D93E9934990687C8ED5421E6537FFB_AdjustorThunk (RuntimeObject* __this, const RuntimeMethod* method)
{
	XmlReaderDebuggerDisplayProxy_tCBCA1821E9EDC752D1884A7A4343C3B0C7134AC9* _thisAdjusted;
	int32_t _offset = 1;
	_thisAdjusted = reinterpret_cast<XmlReaderDebuggerDisplayProxy_tCBCA1821E9EDC752D1884A7A4343C3B0C7134AC9*>(__this + _offset);
	String_t* _returnValue;
	_returnValue = XmlReaderDebuggerDisplayProxy_ToString_mEBDAEFA455D93E9934990687C8ED5421E6537FFB(_thisAdjusted, method);
	return _returnValue;
}
#ifdef __clang__
#pragma clang diagnostic pop
#endif
#ifdef __clang__
#pragma clang diagnostic push
#pragma clang diagnostic ignored "-Winvalid-offsetof"
#pragma clang diagnostic ignored "-Wunused-variable"
#endif
// Method Definition Index: 93869
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR int32_t XmlAttribute_get_LocalNameHash_m09745CA0C4CEA4D07864D977FF90DEBD43C0AE46 (XmlAttribute_t4B6CC461196FBF5CC9F777E74CC82C98E0CA9D18* __this, const RuntimeMethod* method) 
{
	{
		XmlName_t0704430D24D202146901D342E34D878246E14F33* L_0 = __this->___name;
		NullCheck(L_0);
		int32_t L_1;
		L_1 = XmlName_get_HashCode_m329A9FEAF3BDEAA5F1F40CB84F7E51E1BDE58B2D_inline(L_0, NULL);
		return L_1;
	}
}
// Method Definition Index: 93870
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR String_t* XmlAttribute_get_Name_m97BC4F92BC4CB9D0580EFA1339FD713B6C31DBC2 (XmlAttribute_t4B6CC461196FBF5CC9F777E74CC82C98E0CA9D18* __this, const RuntimeMethod* method) 
{
	{
		XmlName_t0704430D24D202146901D342E34D878246E14F33* L_0 = __this->___name;
		NullCheck(L_0);
		String_t* L_1;
		L_1 = XmlName_get_Name_m77B33DD36549E6E3E728EA1AD3325E339EC30435(L_0, NULL);
		return L_1;
	}
}
// Method Definition Index: 93871
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR String_t* XmlAttribute_get_LocalName_mC876CE8B71B1C4F72035B2843E3EE0FACB5FB3A3 (XmlAttribute_t4B6CC461196FBF5CC9F777E74CC82C98E0CA9D18* __this, const RuntimeMethod* method) 
{
	{
		XmlName_t0704430D24D202146901D342E34D878246E14F33* L_0 = __this->___name;
		NullCheck(L_0);
		String_t* L_1;
		L_1 = XmlName_get_LocalName_mD4DE8695A21CB393834F0DDFDD6D6B610E91A64D_inline(L_0, NULL);
		return L_1;
	}
}
// Method Definition Index: 93872
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR int32_t XmlAttribute_get_NodeType_m8B876087E6C0E79E6E02A25D0580E678F0D8A14C (XmlAttribute_t4B6CC461196FBF5CC9F777E74CC82C98E0CA9D18* __this, const RuntimeMethod* method) 
{
	{
		return (int32_t)(2);
	}
}
#ifdef __clang__
#pragma clang diagnostic pop
#endif
#ifdef __clang__
#pragma clang diagnostic push
#pragma clang diagnostic ignored "-Winvalid-offsetof"
#pragma clang diagnostic ignored "-Wunused-variable"
#endif
// Method Definition Index: 93873
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR XmlAttribute_t4B6CC461196FBF5CC9F777E74CC82C98E0CA9D18* XmlAttributeCollection_get_ItemOf_m8F968C39C03DFEB9998B2122F4A26B76B1DB2EDB (XmlAttributeCollection_tB0BA25B3C7E8D4BCF487C7107A9019632A7D85C0* __this, int32_t ___0_i, const RuntimeMethod* method) 
{
	static bool s_Il2CppMethodInitialized;
	if (!s_Il2CppMethodInitialized)
	{
		il2cpp_codegen_initialize_runtime_metadata((uintptr_t*)&XmlAttribute_t4B6CC461196FBF5CC9F777E74CC82C98E0CA9D18_il2cpp_TypeInfo_var);
		s_Il2CppMethodInitialized = true;
	}
	XmlAttribute_t4B6CC461196FBF5CC9F777E74CC82C98E0CA9D18* V_0 = NULL;
	il2cpp::utils::ExceptionSupportStack<RuntimeObject*, 1> __active_exceptions;
	try
	{
		SmallXmlNodeList_tAF536F4BFCA26BF1B0E912818E921A3181D49EF0* L_0 = (SmallXmlNodeList_tAF536F4BFCA26BF1B0E912818E921A3181D49EF0*)(&((XmlNamedNodeMap_t13203D1B3861C62568AFFA1D644C04ABCBFC130C*)__this)->___nodes);
		int32_t L_1 = ___0_i;
		RuntimeObject* L_2;
		L_2 = SmallXmlNodeList_get_Item_mFE828A47A297981D034EFC9244E53B3D34FC9576(L_0, L_1, NULL);
		V_0 = ((XmlAttribute_t4B6CC461196FBF5CC9F777E74CC82C98E0CA9D18*)CastclassClass((RuntimeObject*)L_2, XmlAttribute_t4B6CC461196FBF5CC9F777E74CC82C98E0CA9D18_il2cpp_TypeInfo_var));
		goto IL_0025;
	}
	catch(Il2CppExceptionWrapper& e)
	{
		if(il2cpp_codegen_class_is_assignable_from (((RuntimeClass*)il2cpp_codegen_initialize_runtime_metadata_inline((uintptr_t*)&ArgumentOutOfRangeException_tEA2822DAF62B10EEED00E0E3A341D4BAF78CF85F_il2cpp_TypeInfo_var)), il2cpp_codegen_object_class(e.ex)))
		{
			IL2CPP_PUSH_ACTIVE_EXCEPTION(e.ex);
			goto CATCH_0014;
		}
		throw e;
	}

CATCH_0014:
	{
		ArgumentOutOfRangeException_tEA2822DAF62B10EEED00E0E3A341D4BAF78CF85F* L_3 = ((ArgumentOutOfRangeException_tEA2822DAF62B10EEED00E0E3A341D4BAF78CF85F*)IL2CPP_GET_ACTIVE_EXCEPTION(ArgumentOutOfRangeException_tEA2822DAF62B10EEED00E0E3A341D4BAF78CF85F*));;
		String_t* L_4;
		L_4 = Res_GetString_mBEE82ACFE97F93A6DDE81E322477CF2713466364(((String_t*)il2cpp_codegen_initialize_runtime_metadata_inline((uintptr_t*)&_stringLiteral3C67F6F3D5AD1DF5DD1E3B7F8583419767796154)), NULL);
		IndexOutOfRangeException_t7ECB35264FB6CA8FAA516BD958F4B2ADC78E8A82* L_5 = (IndexOutOfRangeException_t7ECB35264FB6CA8FAA516BD958F4B2ADC78E8A82*)il2cpp_codegen_object_new(((RuntimeClass*)il2cpp_codegen_initialize_runtime_metadata_inline((uintptr_t*)&IndexOutOfRangeException_t7ECB35264FB6CA8FAA516BD958F4B2ADC78E8A82_il2cpp_TypeInfo_var)));
		IndexOutOfRangeException__ctor_mFD06819F05B815BE2D6E826D4E04F4C449D0A425(L_5, L_4, NULL);
		IL2CPP_POP_ACTIVE_EXCEPTION(Exception_t*);
		IL2CPP_RAISE_MANAGED_EXCEPTION(L_5, ((RuntimeMethod*)il2cpp_codegen_initialize_runtime_metadata_inline((uintptr_t*)&XmlAttributeCollection_get_ItemOf_m8F968C39C03DFEB9998B2122F4A26B76B1DB2EDB_RuntimeMethod_var)));
	}

IL_0025:
	{
		XmlAttribute_t4B6CC461196FBF5CC9F777E74CC82C98E0CA9D18* L_6 = V_0;
		return L_6;
	}
}
// Method Definition Index: 93874
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR XmlAttribute_t4B6CC461196FBF5CC9F777E74CC82C98E0CA9D18* XmlAttributeCollection_get_ItemOf_mE6997455F03EC8A99F4ECAFF32E4F212BE70D7CF (XmlAttributeCollection_tB0BA25B3C7E8D4BCF487C7107A9019632A7D85C0* __this, String_t* ___0_name, const RuntimeMethod* method) 
{
	static bool s_Il2CppMethodInitialized;
	if (!s_Il2CppMethodInitialized)
	{
		il2cpp_codegen_initialize_runtime_metadata((uintptr_t*)&XmlAttribute_t4B6CC461196FBF5CC9F777E74CC82C98E0CA9D18_il2cpp_TypeInfo_var);
		s_Il2CppMethodInitialized = true;
	}
	int32_t V_0 = 0;
	int32_t V_1 = 0;
	XmlAttribute_t4B6CC461196FBF5CC9F777E74CC82C98E0CA9D18* V_2 = NULL;
	{
		String_t* L_0 = ___0_name;
		int32_t L_1;
		L_1 = XmlName_GetHashCode_mC2C5513D62B0E10E556D1241BC33C4AFFAD1DDCD(L_0, NULL);
		V_0 = L_1;
		V_1 = 0;
		goto IL_003a;
	}

IL_000b:
	{
		SmallXmlNodeList_tAF536F4BFCA26BF1B0E912818E921A3181D49EF0* L_2 = (SmallXmlNodeList_tAF536F4BFCA26BF1B0E912818E921A3181D49EF0*)(&((XmlNamedNodeMap_t13203D1B3861C62568AFFA1D644C04ABCBFC130C*)__this)->___nodes);
		int32_t L_3 = V_1;
		RuntimeObject* L_4;
		L_4 = SmallXmlNodeList_get_Item_mFE828A47A297981D034EFC9244E53B3D34FC9576(L_2, L_3, NULL);
		V_2 = ((XmlAttribute_t4B6CC461196FBF5CC9F777E74CC82C98E0CA9D18*)CastclassClass((RuntimeObject*)L_4, XmlAttribute_t4B6CC461196FBF5CC9F777E74CC82C98E0CA9D18_il2cpp_TypeInfo_var));
		int32_t L_5 = V_0;
		XmlAttribute_t4B6CC461196FBF5CC9F777E74CC82C98E0CA9D18* L_6 = V_2;
		NullCheck(L_6);
		int32_t L_7;
		L_7 = XmlAttribute_get_LocalNameHash_m09745CA0C4CEA4D07864D977FF90DEBD43C0AE46(L_6, NULL);
		if ((!(((uint32_t)L_5) == ((uint32_t)L_7))))
		{
			goto IL_0036;
		}
	}
	{
		String_t* L_8 = ___0_name;
		XmlAttribute_t4B6CC461196FBF5CC9F777E74CC82C98E0CA9D18* L_9 = V_2;
		NullCheck(L_9);
		String_t* L_10;
		L_10 = VirtualFuncInvoker0< String_t* >::Invoke(4, L_9);
		bool L_11;
		L_11 = String_op_Equality_m030E1B219352228970A076136E455C4E568C02C1(L_8, L_10, NULL);
		if (!L_11)
		{
			goto IL_0036;
		}
	}
	{
		XmlAttribute_t4B6CC461196FBF5CC9F777E74CC82C98E0CA9D18* L_12 = V_2;
		return L_12;
	}

IL_0036:
	{
		int32_t L_13 = V_1;
		V_1 = ((int32_t)il2cpp_codegen_add(L_13, 1));
	}

IL_003a:
	{
		int32_t L_14 = V_1;
		SmallXmlNodeList_tAF536F4BFCA26BF1B0E912818E921A3181D49EF0* L_15 = (SmallXmlNodeList_tAF536F4BFCA26BF1B0E912818E921A3181D49EF0*)(&((XmlNamedNodeMap_t13203D1B3861C62568AFFA1D644C04ABCBFC130C*)__this)->___nodes);
		int32_t L_16;
		L_16 = SmallXmlNodeList_get_Count_m79F61C89FC8ABD73BCB56938310461EFF1DF14BF(L_15, NULL);
		if ((((int32_t)L_14) < ((int32_t)L_16)))
		{
			goto IL_000b;
		}
	}
	{
		return (XmlAttribute_t4B6CC461196FBF5CC9F777E74CC82C98E0CA9D18*)NULL;
	}
}
#ifdef __clang__
#pragma clang diagnostic pop
#endif
#ifdef __clang__
#pragma clang diagnostic push
#pragma clang diagnostic ignored "-Winvalid-offsetof"
#pragma clang diagnostic ignored "-Wunused-variable"
#endif
// Method Definition Index: 93875
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR void XmlChildEnumerator__ctor_mC51E8B709CB2FCB079D44B180D5EF3DF9DBC3671 (XmlChildEnumerator_tDD03B83EAA250AB86DFF4B53C1430DA942E85939* __this, XmlNode_t3180B9B3D5C36CD58F5327D9F13458E3B3F030AF* ___0_container, const RuntimeMethod* method) 
{
	{
		Object__ctor_mE837C6B9FA8C6D5D109F4B2EC885D79919AC0EA2(__this, NULL);
		XmlNode_t3180B9B3D5C36CD58F5327D9F13458E3B3F030AF* L_0 = ___0_container;
		__this->___container = L_0;
		Il2CppCodeGenWriteBarrier((void**)(&__this->___container), (void*)L_0);
		XmlNode_t3180B9B3D5C36CD58F5327D9F13458E3B3F030AF* L_1 = ___0_container;
		NullCheck(L_1);
		XmlNode_t3180B9B3D5C36CD58F5327D9F13458E3B3F030AF* L_2;
		L_2 = VirtualFuncInvoker0< XmlNode_t3180B9B3D5C36CD58F5327D9F13458E3B3F030AF* >::Invoke(13, L_1);
		__this->___child = L_2;
		Il2CppCodeGenWriteBarrier((void**)(&__this->___child), (void*)L_2);
		__this->___isFirst = (bool)1;
		return;
	}
}
// Method Definition Index: 93876
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR bool XmlChildEnumerator_System_Collections_IEnumerator_MoveNext_m9DF8D71649BD5173C9E9859A12E9E6777450A55D (XmlChildEnumerator_tDD03B83EAA250AB86DFF4B53C1430DA942E85939* __this, const RuntimeMethod* method) 
{
	{
		bool L_0;
		L_0 = XmlChildEnumerator_MoveNext_m038B637C00AC0482CAD4E35995F2CF2AE8E3D556(__this, NULL);
		return L_0;
	}
}
// Method Definition Index: 93877
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR bool XmlChildEnumerator_MoveNext_m038B637C00AC0482CAD4E35995F2CF2AE8E3D556 (XmlChildEnumerator_tDD03B83EAA250AB86DFF4B53C1430DA942E85939* __this, const RuntimeMethod* method) 
{
	{
		bool L_0 = __this->___isFirst;
		if (!L_0)
		{
			goto IL_0022;
		}
	}
	{
		XmlNode_t3180B9B3D5C36CD58F5327D9F13458E3B3F030AF* L_1 = __this->___container;
		NullCheck(L_1);
		XmlNode_t3180B9B3D5C36CD58F5327D9F13458E3B3F030AF* L_2;
		L_2 = VirtualFuncInvoker0< XmlNode_t3180B9B3D5C36CD58F5327D9F13458E3B3F030AF* >::Invoke(13, L_1);
		__this->___child = L_2;
		Il2CppCodeGenWriteBarrier((void**)(&__this->___child), (void*)L_2);
		__this->___isFirst = (bool)0;
		goto IL_003b;
	}

IL_0022:
	{
		XmlNode_t3180B9B3D5C36CD58F5327D9F13458E3B3F030AF* L_3 = __this->___child;
		if (!L_3)
		{
			goto IL_003b;
		}
	}
	{
		XmlNode_t3180B9B3D5C36CD58F5327D9F13458E3B3F030AF* L_4 = __this->___child;
		NullCheck(L_4);
		XmlNode_t3180B9B3D5C36CD58F5327D9F13458E3B3F030AF* L_5;
		L_5 = VirtualFuncInvoker0< XmlNode_t3180B9B3D5C36CD58F5327D9F13458E3B3F030AF* >::Invoke(10, L_4);
		__this->___child = L_5;
		Il2CppCodeGenWriteBarrier((void**)(&__this->___child), (void*)L_5);
	}

IL_003b:
	{
		XmlNode_t3180B9B3D5C36CD58F5327D9F13458E3B3F030AF* L_6 = __this->___child;
		return (bool)((!(((RuntimeObject*)(XmlNode_t3180B9B3D5C36CD58F5327D9F13458E3B3F030AF*)L_6) <= ((RuntimeObject*)(RuntimeObject*)NULL)))? 1 : 0);
	}
}
// Method Definition Index: 93878
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR void XmlChildEnumerator_System_Collections_IEnumerator_Reset_m3E1BD7C0BE0E5576593A8C53ABC11109F275C4EF (XmlChildEnumerator_tDD03B83EAA250AB86DFF4B53C1430DA942E85939* __this, const RuntimeMethod* method) 
{
	{
		__this->___isFirst = (bool)1;
		XmlNode_t3180B9B3D5C36CD58F5327D9F13458E3B3F030AF* L_0 = __this->___container;
		NullCheck(L_0);
		XmlNode_t3180B9B3D5C36CD58F5327D9F13458E3B3F030AF* L_1;
		L_1 = VirtualFuncInvoker0< XmlNode_t3180B9B3D5C36CD58F5327D9F13458E3B3F030AF* >::Invoke(13, L_0);
		__this->___child = L_1;
		Il2CppCodeGenWriteBarrier((void**)(&__this->___child), (void*)L_1);
		return;
	}
}
// Method Definition Index: 93879
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR RuntimeObject* XmlChildEnumerator_System_Collections_IEnumerator_get_Current_mEC0FD9B4CD70461AD71D93AB21025A3D83AE0418 (XmlChildEnumerator_tDD03B83EAA250AB86DFF4B53C1430DA942E85939* __this, const RuntimeMethod* method) 
{
	{
		XmlNode_t3180B9B3D5C36CD58F5327D9F13458E3B3F030AF* L_0;
		L_0 = XmlChildEnumerator_get_Current_m149208EFC92AEDC42F82839102B3D5F017DA4C46(__this, NULL);
		return L_0;
	}
}
// Method Definition Index: 93880
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR XmlNode_t3180B9B3D5C36CD58F5327D9F13458E3B3F030AF* XmlChildEnumerator_get_Current_m149208EFC92AEDC42F82839102B3D5F017DA4C46 (XmlChildEnumerator_tDD03B83EAA250AB86DFF4B53C1430DA942E85939* __this, const RuntimeMethod* method) 
{
	{
		bool L_0 = __this->___isFirst;
		if (L_0)
		{
			goto IL_0010;
		}
	}
	{
		XmlNode_t3180B9B3D5C36CD58F5327D9F13458E3B3F030AF* L_1 = __this->___child;
		if (L_1)
		{
			goto IL_0020;
		}
	}

IL_0010:
	{
		String_t* L_2;
		L_2 = Res_GetString_mBEE82ACFE97F93A6DDE81E322477CF2713466364(((String_t*)il2cpp_codegen_initialize_runtime_metadata_inline((uintptr_t*)&_stringLiteralFBD1A9745DCC4D80697D44756437CCB57698AE21)), NULL);
		InvalidOperationException_t5DDE4D49B7405FAAB1E4576F4715A42A3FAD4BAB* L_3 = (InvalidOperationException_t5DDE4D49B7405FAAB1E4576F4715A42A3FAD4BAB*)il2cpp_codegen_object_new(((RuntimeClass*)il2cpp_codegen_initialize_runtime_metadata_inline((uintptr_t*)&InvalidOperationException_t5DDE4D49B7405FAAB1E4576F4715A42A3FAD4BAB_il2cpp_TypeInfo_var)));
		InvalidOperationException__ctor_mE4CB6F4712AB6D99A2358FBAE2E052B3EE976162(L_3, L_2, NULL);
		IL2CPP_RAISE_MANAGED_EXCEPTION(L_3, ((RuntimeMethod*)il2cpp_codegen_initialize_runtime_metadata_inline((uintptr_t*)&XmlChildEnumerator_get_Current_m149208EFC92AEDC42F82839102B3D5F017DA4C46_RuntimeMethod_var)));
	}

IL_0020:
	{
		XmlNode_t3180B9B3D5C36CD58F5327D9F13458E3B3F030AF* L_4 = __this->___child;
		return L_4;
	}
}
#ifdef __clang__
#pragma clang diagnostic pop
#endif
#ifdef __clang__
#pragma clang diagnostic push
#pragma clang diagnostic ignored "-Winvalid-offsetof"
#pragma clang diagnostic ignored "-Wunused-variable"
#endif
// Method Definition Index: 93881
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR void XmlChildNodes__ctor_mE297CC7E680D376A26339D6067C7E434271234B5 (XmlChildNodes_tBD3126123F4810D8B272B8201C07FE70CDC9C5E2* __this, XmlNode_t3180B9B3D5C36CD58F5327D9F13458E3B3F030AF* ___0_container, const RuntimeMethod* method) 
{
	{
		XmlNodeList__ctor_m29940F85BF8D20E6FFB9732648DC6DAD10539BD3(__this, NULL);
		XmlNode_t3180B9B3D5C36CD58F5327D9F13458E3B3F030AF* L_0 = ___0_container;
		__this->___container = L_0;
		Il2CppCodeGenWriteBarrier((void**)(&__this->___container), (void*)L_0);
		return;
	}
}
// Method Definition Index: 93882
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR RuntimeObject* XmlChildNodes_GetEnumerator_m79ACE972A13482959B5E446059C899FC9988A81C (XmlChildNodes_tBD3126123F4810D8B272B8201C07FE70CDC9C5E2* __this, const RuntimeMethod* method) 
{
	static bool s_Il2CppMethodInitialized;
	if (!s_Il2CppMethodInitialized)
	{
		il2cpp_codegen_initialize_runtime_metadata((uintptr_t*)&XmlChildEnumerator_tDD03B83EAA250AB86DFF4B53C1430DA942E85939_il2cpp_TypeInfo_var);
		il2cpp_codegen_initialize_runtime_metadata((uintptr_t*)&XmlDocument_t4DE82998E642C5C21A4A620A5278237C70D3E42B_il2cpp_TypeInfo_var);
		s_Il2CppMethodInitialized = true;
	}
	{
		XmlNode_t3180B9B3D5C36CD58F5327D9F13458E3B3F030AF* L_0 = __this->___container;
		NullCheck(L_0);
		XmlNode_t3180B9B3D5C36CD58F5327D9F13458E3B3F030AF* L_1;
		L_1 = VirtualFuncInvoker0< XmlNode_t3180B9B3D5C36CD58F5327D9F13458E3B3F030AF* >::Invoke(13, L_0);
		if (L_1)
		{
			goto IL_0013;
		}
	}
	{
		il2cpp_codegen_runtime_class_init_inline(XmlDocument_t4DE82998E642C5C21A4A620A5278237C70D3E42B_il2cpp_TypeInfo_var);
		EmptyEnumerator_t84EC9187C8460EE98E675ED9258AE4DF2A6776DA* L_2 = ((XmlDocument_t4DE82998E642C5C21A4A620A5278237C70D3E42B_StaticFields*)il2cpp_codegen_static_fields_for(XmlDocument_t4DE82998E642C5C21A4A620A5278237C70D3E42B_il2cpp_TypeInfo_var))->___EmptyEnumerator;
		return L_2;
	}

IL_0013:
	{
		XmlNode_t3180B9B3D5C36CD58F5327D9F13458E3B3F030AF* L_3 = __this->___container;
		XmlChildEnumerator_tDD03B83EAA250AB86DFF4B53C1430DA942E85939* L_4 = (XmlChildEnumerator_tDD03B83EAA250AB86DFF4B53C1430DA942E85939*)il2cpp_codegen_object_new(XmlChildEnumerator_tDD03B83EAA250AB86DFF4B53C1430DA942E85939_il2cpp_TypeInfo_var);
		XmlChildEnumerator__ctor_mC51E8B709CB2FCB079D44B180D5EF3DF9DBC3671(L_4, L_3, NULL);
		return L_4;
	}
}
#ifdef __clang__
#pragma clang diagnostic pop
#endif
#ifdef __clang__
#pragma clang diagnostic push
#pragma clang diagnostic ignored "-Winvalid-offsetof"
#pragma clang diagnostic ignored "-Wunused-variable"
#endif
// Method Definition Index: 93883
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR int32_t XmlDocument_get_NodeType_m49BC1016CABB8F3949B923DA9E6C89EEB676BC37 (XmlDocument_t4DE82998E642C5C21A4A620A5278237C70D3E42B* __this, const RuntimeMethod* method) 
{
	{
		return (int32_t)(((int32_t)9));
	}
}
// Method Definition Index: 93884
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR String_t* XmlDocument_get_Name_mDD3B83B70FA0065CE4959C41BD569084E2219C35 (XmlDocument_t4DE82998E642C5C21A4A620A5278237C70D3E42B* __this, const RuntimeMethod* method) 
{
	{
		String_t* L_0 = __this->___strDocumentName;
		return L_0;
	}
}
// Method Definition Index: 93885
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR String_t* XmlDocument_get_LocalName_mF25545A8C83B64CB8E28DC2983997B2F860AD5A9 (XmlDocument_t4DE82998E642C5C21A4A620A5278237C70D3E42B* __this, const RuntimeMethod* method) 
{
	{
		String_t* L_0 = __this->___strDocumentName;
		return L_0;
	}
}
// Method Definition Index: 93886
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR XmlNodeList_tFC0CEED3A006BEDF6A1A420F7CB55AC33E9B7F85* XmlDocument_GetElementsByTagName_m2553763C9CE3E5DAC070E91D271FDD2923A99544 (XmlDocument_t4DE82998E642C5C21A4A620A5278237C70D3E42B* __this, String_t* ___0_name, const RuntimeMethod* method) 
{
	static bool s_Il2CppMethodInitialized;
	if (!s_Il2CppMethodInitialized)
	{
		il2cpp_codegen_initialize_runtime_metadata((uintptr_t*)&XmlElementList_tFE0D5337668FDE708796D7FDB669AE4A290CAB57_il2cpp_TypeInfo_var);
		s_Il2CppMethodInitialized = true;
	}
	{
		String_t* L_0 = ___0_name;
		XmlElementList_tFE0D5337668FDE708796D7FDB669AE4A290CAB57* L_1 = (XmlElementList_tFE0D5337668FDE708796D7FDB669AE4A290CAB57*)il2cpp_codegen_object_new(XmlElementList_tFE0D5337668FDE708796D7FDB669AE4A290CAB57_il2cpp_TypeInfo_var);
		XmlElementList__ctor_m38E1065BC153887F600A64F0FAF6D595A928BCB1(L_1, __this, L_0, NULL);
		return L_1;
	}
}
// Method Definition Index: 93887
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR XmlNameTable_tBDBAACFF3DB40A8E6AF3BDC11F0FF166CF11ABB8* XmlDocument_get_NameTable_m4B913865A24AEA917172F75CBDCE94C81CCB7E2C (XmlDocument_t4DE82998E642C5C21A4A620A5278237C70D3E42B* __this, const RuntimeMethod* method) 
{
	{
		XmlImplementation_t4B3F467B76BD95C919C40424196C55B38EEC0F4D* L_0 = __this->___implementation;
		NullCheck(L_0);
		XmlNameTable_tBDBAACFF3DB40A8E6AF3BDC11F0FF166CF11ABB8* L_1;
		L_1 = XmlImplementation_get_NameTable_m8951702F59619310BD4A9528D2FF471352518CDF_inline(L_0, NULL);
		return L_1;
	}
}
// Method Definition Index: 93888
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR void XmlDocument_add_NodeInserted_m5B03345EAC48958B104BA2033818370BA6E2243D (XmlDocument_t4DE82998E642C5C21A4A620A5278237C70D3E42B* __this, XmlNodeChangedEventHandler_t907F6E9CB8DE238635028EC468AD41CAB0BA269B* ___0_value, const RuntimeMethod* method) 
{
	static bool s_Il2CppMethodInitialized;
	if (!s_Il2CppMethodInitialized)
	{
		il2cpp_codegen_initialize_runtime_metadata((uintptr_t*)&XmlNodeChangedEventHandler_t907F6E9CB8DE238635028EC468AD41CAB0BA269B_il2cpp_TypeInfo_var);
		s_Il2CppMethodInitialized = true;
	}
	{
		XmlNodeChangedEventHandler_t907F6E9CB8DE238635028EC468AD41CAB0BA269B* L_0 = __this->___onNodeInsertedDelegate;
		XmlNodeChangedEventHandler_t907F6E9CB8DE238635028EC468AD41CAB0BA269B* L_1 = ___0_value;
		Delegate_t* L_2;
		L_2 = Delegate_Combine_m1F725AEF318BE6F0426863490691A6F4606E7D00(L_0, L_1, NULL);
		__this->___onNodeInsertedDelegate = ((XmlNodeChangedEventHandler_t907F6E9CB8DE238635028EC468AD41CAB0BA269B*)CastclassSealed((RuntimeObject*)L_2, XmlNodeChangedEventHandler_t907F6E9CB8DE238635028EC468AD41CAB0BA269B_il2cpp_TypeInfo_var));
		Il2CppCodeGenWriteBarrier((void**)(&__this->___onNodeInsertedDelegate), (void*)((XmlNodeChangedEventHandler_t907F6E9CB8DE238635028EC468AD41CAB0BA269B*)CastclassSealed((RuntimeObject*)L_2, XmlNodeChangedEventHandler_t907F6E9CB8DE238635028EC468AD41CAB0BA269B_il2cpp_TypeInfo_var)));
		return;
	}
}
// Method Definition Index: 93889
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR void XmlDocument_remove_NodeInserted_mF46D0B84DB934188F551BCC766080CDA9A787616 (XmlDocument_t4DE82998E642C5C21A4A620A5278237C70D3E42B* __this, XmlNodeChangedEventHandler_t907F6E9CB8DE238635028EC468AD41CAB0BA269B* ___0_value, const RuntimeMethod* method) 
{
	static bool s_Il2CppMethodInitialized;
	if (!s_Il2CppMethodInitialized)
	{
		il2cpp_codegen_initialize_runtime_metadata((uintptr_t*)&XmlNodeChangedEventHandler_t907F6E9CB8DE238635028EC468AD41CAB0BA269B_il2cpp_TypeInfo_var);
		s_Il2CppMethodInitialized = true;
	}
	{
		XmlNodeChangedEventHandler_t907F6E9CB8DE238635028EC468AD41CAB0BA269B* L_0 = __this->___onNodeInsertedDelegate;
		XmlNodeChangedEventHandler_t907F6E9CB8DE238635028EC468AD41CAB0BA269B* L_1 = ___0_value;
		Delegate_t* L_2;
		L_2 = Delegate_Remove_m8B7DD5661308FA972E23CA1CC3FC9CEB355504E3(L_0, L_1, NULL);
		__this->___onNodeInsertedDelegate = ((XmlNodeChangedEventHandler_t907F6E9CB8DE238635028EC468AD41CAB0BA269B*)CastclassSealed((RuntimeObject*)L_2, XmlNodeChangedEventHandler_t907F6E9CB8DE238635028EC468AD41CAB0BA269B_il2cpp_TypeInfo_var));
		Il2CppCodeGenWriteBarrier((void**)(&__this->___onNodeInsertedDelegate), (void*)((XmlNodeChangedEventHandler_t907F6E9CB8DE238635028EC468AD41CAB0BA269B*)CastclassSealed((RuntimeObject*)L_2, XmlNodeChangedEventHandler_t907F6E9CB8DE238635028EC468AD41CAB0BA269B_il2cpp_TypeInfo_var)));
		return;
	}
}
// Method Definition Index: 93890
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR void XmlDocument_add_NodeRemoved_m30754B20BE8A674BD5FDECF175E2419DD2AE3E1C (XmlDocument_t4DE82998E642C5C21A4A620A5278237C70D3E42B* __this, XmlNodeChangedEventHandler_t907F6E9CB8DE238635028EC468AD41CAB0BA269B* ___0_value, const RuntimeMethod* method) 
{
	static bool s_Il2CppMethodInitialized;
	if (!s_Il2CppMethodInitialized)
	{
		il2cpp_codegen_initialize_runtime_metadata((uintptr_t*)&XmlNodeChangedEventHandler_t907F6E9CB8DE238635028EC468AD41CAB0BA269B_il2cpp_TypeInfo_var);
		s_Il2CppMethodInitialized = true;
	}
	{
		XmlNodeChangedEventHandler_t907F6E9CB8DE238635028EC468AD41CAB0BA269B* L_0 = __this->___onNodeRemovedDelegate;
		XmlNodeChangedEventHandler_t907F6E9CB8DE238635028EC468AD41CAB0BA269B* L_1 = ___0_value;
		Delegate_t* L_2;
		L_2 = Delegate_Combine_m1F725AEF318BE6F0426863490691A6F4606E7D00(L_0, L_1, NULL);
		__this->___onNodeRemovedDelegate = ((XmlNodeChangedEventHandler_t907F6E9CB8DE238635028EC468AD41CAB0BA269B*)CastclassSealed((RuntimeObject*)L_2, XmlNodeChangedEventHandler_t907F6E9CB8DE238635028EC468AD41CAB0BA269B_il2cpp_TypeInfo_var));
		Il2CppCodeGenWriteBarrier((void**)(&__this->___onNodeRemovedDelegate), (void*)((XmlNodeChangedEventHandler_t907F6E9CB8DE238635028EC468AD41CAB0BA269B*)CastclassSealed((RuntimeObject*)L_2, XmlNodeChangedEventHandler_t907F6E9CB8DE238635028EC468AD41CAB0BA269B_il2cpp_TypeInfo_var)));
		return;
	}
}
// Method Definition Index: 93891
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR void XmlDocument_remove_NodeRemoved_m33B3778BE1EB94A010FDE8F860641821A7A079A3 (XmlDocument_t4DE82998E642C5C21A4A620A5278237C70D3E42B* __this, XmlNodeChangedEventHandler_t907F6E9CB8DE238635028EC468AD41CAB0BA269B* ___0_value, const RuntimeMethod* method) 
{
	static bool s_Il2CppMethodInitialized;
	if (!s_Il2CppMethodInitialized)
	{
		il2cpp_codegen_initialize_runtime_metadata((uintptr_t*)&XmlNodeChangedEventHandler_t907F6E9CB8DE238635028EC468AD41CAB0BA269B_il2cpp_TypeInfo_var);
		s_Il2CppMethodInitialized = true;
	}
	{
		XmlNodeChangedEventHandler_t907F6E9CB8DE238635028EC468AD41CAB0BA269B* L_0 = __this->___onNodeRemovedDelegate;
		XmlNodeChangedEventHandler_t907F6E9CB8DE238635028EC468AD41CAB0BA269B* L_1 = ___0_value;
		Delegate_t* L_2;
		L_2 = Delegate_Remove_m8B7DD5661308FA972E23CA1CC3FC9CEB355504E3(L_0, L_1, NULL);
		__this->___onNodeRemovedDelegate = ((XmlNodeChangedEventHandler_t907F6E9CB8DE238635028EC468AD41CAB0BA269B*)CastclassSealed((RuntimeObject*)L_2, XmlNodeChangedEventHandler_t907F6E9CB8DE238635028EC468AD41CAB0BA269B_il2cpp_TypeInfo_var));
		Il2CppCodeGenWriteBarrier((void**)(&__this->___onNodeRemovedDelegate), (void*)((XmlNodeChangedEventHandler_t907F6E9CB8DE238635028EC468AD41CAB0BA269B*)CastclassSealed((RuntimeObject*)L_2, XmlNodeChangedEventHandler_t907F6E9CB8DE238635028EC468AD41CAB0BA269B_il2cpp_TypeInfo_var)));
		return;
	}
}
// Method Definition Index: 93892
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR void XmlDocument__cctor_m27626C1CF5AC7AB088D03782DE104EB671099C45 (const RuntimeMethod* method) 
{
	static bool s_Il2CppMethodInitialized;
	if (!s_Il2CppMethodInitialized)
	{
		il2cpp_codegen_initialize_runtime_metadata((uintptr_t*)&EmptyEnumerator_t84EC9187C8460EE98E675ED9258AE4DF2A6776DA_il2cpp_TypeInfo_var);
		il2cpp_codegen_initialize_runtime_metadata((uintptr_t*)&XmlDocument_t4DE82998E642C5C21A4A620A5278237C70D3E42B_il2cpp_TypeInfo_var);
		il2cpp_codegen_initialize_runtime_metadata((uintptr_t*)&XmlSchemaInfo_t6686870BCAA40A8EFD7987976F386DD40DD5323B_il2cpp_TypeInfo_var);
		s_Il2CppMethodInitialized = true;
	}
	{
		EmptyEnumerator_t84EC9187C8460EE98E675ED9258AE4DF2A6776DA* L_0 = (EmptyEnumerator_t84EC9187C8460EE98E675ED9258AE4DF2A6776DA*)il2cpp_codegen_object_new(EmptyEnumerator_t84EC9187C8460EE98E675ED9258AE4DF2A6776DA_il2cpp_TypeInfo_var);
		EmptyEnumerator__ctor_m05DA73C42354A4E549CCA7E9485B294057ABA981(L_0, NULL);
		((XmlDocument_t4DE82998E642C5C21A4A620A5278237C70D3E42B_StaticFields*)il2cpp_codegen_static_fields_for(XmlDocument_t4DE82998E642C5C21A4A620A5278237C70D3E42B_il2cpp_TypeInfo_var))->___EmptyEnumerator = L_0;
		Il2CppCodeGenWriteBarrier((void**)(&((XmlDocument_t4DE82998E642C5C21A4A620A5278237C70D3E42B_StaticFields*)il2cpp_codegen_static_fields_for(XmlDocument_t4DE82998E642C5C21A4A620A5278237C70D3E42B_il2cpp_TypeInfo_var))->___EmptyEnumerator), (void*)L_0);
		XmlSchemaInfo_t6686870BCAA40A8EFD7987976F386DD40DD5323B* L_1 = (XmlSchemaInfo_t6686870BCAA40A8EFD7987976F386DD40DD5323B*)il2cpp_codegen_object_new(XmlSchemaInfo_t6686870BCAA40A8EFD7987976F386DD40DD5323B_il2cpp_TypeInfo_var);
		XmlSchemaInfo__ctor_mEB56BD698D44A8AAA47668230E0CDFE8463ABD08(L_1, 0, NULL);
		((XmlDocument_t4DE82998E642C5C21A4A620A5278237C70D3E42B_StaticFields*)il2cpp_codegen_static_fields_for(XmlDocument_t4DE82998E642C5C21A4A620A5278237C70D3E42B_il2cpp_TypeInfo_var))->___NotKnownSchemaInfo = L_1;
		Il2CppCodeGenWriteBarrier((void**)(&((XmlDocument_t4DE82998E642C5C21A4A620A5278237C70D3E42B_StaticFields*)il2cpp_codegen_static_fields_for(XmlDocument_t4DE82998E642C5C21A4A620A5278237C70D3E42B_il2cpp_TypeInfo_var))->___NotKnownSchemaInfo), (void*)L_1);
		XmlSchemaInfo_t6686870BCAA40A8EFD7987976F386DD40DD5323B* L_2 = (XmlSchemaInfo_t6686870BCAA40A8EFD7987976F386DD40DD5323B*)il2cpp_codegen_object_new(XmlSchemaInfo_t6686870BCAA40A8EFD7987976F386DD40DD5323B_il2cpp_TypeInfo_var);
		XmlSchemaInfo__ctor_mEB56BD698D44A8AAA47668230E0CDFE8463ABD08(L_2, 1, NULL);
		((XmlDocument_t4DE82998E642C5C21A4A620A5278237C70D3E42B_StaticFields*)il2cpp_codegen_static_fields_for(XmlDocument_t4DE82998E642C5C21A4A620A5278237C70D3E42B_il2cpp_TypeInfo_var))->___ValidSchemaInfo = L_2;
		Il2CppCodeGenWriteBarrier((void**)(&((XmlDocument_t4DE82998E642C5C21A4A620A5278237C70D3E42B_StaticFields*)il2cpp_codegen_static_fields_for(XmlDocument_t4DE82998E642C5C21A4A620A5278237C70D3E42B_il2cpp_TypeInfo_var))->___ValidSchemaInfo), (void*)L_2);
		XmlSchemaInfo_t6686870BCAA40A8EFD7987976F386DD40DD5323B* L_3 = (XmlSchemaInfo_t6686870BCAA40A8EFD7987976F386DD40DD5323B*)il2cpp_codegen_object_new(XmlSchemaInfo_t6686870BCAA40A8EFD7987976F386DD40DD5323B_il2cpp_TypeInfo_var);
		XmlSchemaInfo__ctor_mEB56BD698D44A8AAA47668230E0CDFE8463ABD08(L_3, 2, NULL);
		((XmlDocument_t4DE82998E642C5C21A4A620A5278237C70D3E42B_StaticFields*)il2cpp_codegen_static_fields_for(XmlDocument_t4DE82998E642C5C21A4A620A5278237C70D3E42B_il2cpp_TypeInfo_var))->___InvalidSchemaInfo = L_3;
		Il2CppCodeGenWriteBarrier((void**)(&((XmlDocument_t4DE82998E642C5C21A4A620A5278237C70D3E42B_StaticFields*)il2cpp_codegen_static_fields_for(XmlDocument_t4DE82998E642C5C21A4A620A5278237C70D3E42B_il2cpp_TypeInfo_var))->___InvalidSchemaInfo), (void*)L_3);
		return;
	}
}
#ifdef __clang__
#pragma clang diagnostic pop
#endif
#ifdef __clang__
#pragma clang diagnostic push
#pragma clang diagnostic ignored "-Winvalid-offsetof"
#pragma clang diagnostic ignored "-Wunused-variable"
#endif
// Method Definition Index: 93893
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR String_t* XmlDocumentType_get_Name_m8323D4846265D13C6E393961B2EE0D7FC96812C2 (XmlDocumentType_tFD7379E45412FC2EBD65280ED3DE7A1C941A8D9E* __this, const RuntimeMethod* method) 
{
	{
		String_t* L_0 = __this->___name;
		return L_0;
	}
}
// Method Definition Index: 93894
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR String_t* XmlDocumentType_get_LocalName_m5B495551922FE2C3CC5285FA6CDE88478891E3B6 (XmlDocumentType_tFD7379E45412FC2EBD65280ED3DE7A1C941A8D9E* __this, const RuntimeMethod* method) 
{
	{
		String_t* L_0 = __this->___name;
		return L_0;
	}
}
// Method Definition Index: 93895
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR int32_t XmlDocumentType_get_NodeType_mF592A32BB36245914C8EC201A2AD5ED0611BCF17 (XmlDocumentType_tFD7379E45412FC2EBD65280ED3DE7A1C941A8D9E* __this, const RuntimeMethod* method) 
{
	{
		return (int32_t)(((int32_t)10));
	}
}
// Method Definition Index: 93896
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR String_t* XmlDocumentType_get_PublicId_m9E81FAF2D0AA9DC847D118813CBEB085F0CE1955 (XmlDocumentType_tFD7379E45412FC2EBD65280ED3DE7A1C941A8D9E* __this, const RuntimeMethod* method) 
{
	{
		String_t* L_0 = __this->___publicId;
		return L_0;
	}
}
// Method Definition Index: 93897
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR String_t* XmlDocumentType_get_SystemId_m17B225C509C0F5A4ECBB838AA81D15DED4016909 (XmlDocumentType_tFD7379E45412FC2EBD65280ED3DE7A1C941A8D9E* __this, const RuntimeMethod* method) 
{
	{
		String_t* L_0 = __this->___systemId;
		return L_0;
	}
}
// Method Definition Index: 93898
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR String_t* XmlDocumentType_get_InternalSubset_mBD8D585ED0401EE1FC02F6FD5445E2109B083FEF (XmlDocumentType_tFD7379E45412FC2EBD65280ED3DE7A1C941A8D9E* __this, const RuntimeMethod* method) 
{
	{
		String_t* L_0 = __this->___internalSubset;
		return L_0;
	}
}
#ifdef __clang__
#pragma clang diagnostic pop
#endif
#ifdef __clang__
#pragma clang diagnostic push
#pragma clang diagnostic ignored "-Winvalid-offsetof"
#pragma clang diagnostic ignored "-Wunused-variable"
#endif
// Method Definition Index: 93899
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR void XmlElementList__ctor_mE9CE5108CF2B91213733B81C26C40C57D5726B07 (XmlElementList_tFE0D5337668FDE708796D7FDB669AE4A290CAB57* __this, XmlNode_t3180B9B3D5C36CD58F5327D9F13458E3B3F030AF* ___0_parent, const RuntimeMethod* method) 
{
	static bool s_Il2CppMethodInitialized;
	if (!s_Il2CppMethodInitialized)
	{
		il2cpp_codegen_initialize_runtime_metadata((uintptr_t*)&WeakReference_tD4B0518CE911FFD9FAAB3FCD492644A354312D8E_il2cpp_TypeInfo_var);
		il2cpp_codegen_initialize_runtime_metadata((uintptr_t*)&XmlElementListListener_t1513DCDD9C9C5B56690342DCC4445CB4BE6E7E79_il2cpp_TypeInfo_var);
		s_Il2CppMethodInitialized = true;
	}
	{
		XmlNodeList__ctor_m29940F85BF8D20E6FFB9732648DC6DAD10539BD3(__this, NULL);
		XmlNode_t3180B9B3D5C36CD58F5327D9F13458E3B3F030AF* L_0 = ___0_parent;
		__this->___rootNode = L_0;
		Il2CppCodeGenWriteBarrier((void**)(&__this->___rootNode), (void*)L_0);
		__this->___curInd = (-1);
		XmlNode_t3180B9B3D5C36CD58F5327D9F13458E3B3F030AF* L_1 = __this->___rootNode;
		__this->___curElem = L_1;
		Il2CppCodeGenWriteBarrier((void**)(&__this->___curElem), (void*)L_1);
		__this->___changeCount = 0;
		__this->___empty = (bool)0;
		__this->___atomized = (bool)1;
		__this->___matchCount = (-1);
		XmlNode_t3180B9B3D5C36CD58F5327D9F13458E3B3F030AF* L_2 = ___0_parent;
		NullCheck(L_2);
		XmlDocument_t4DE82998E642C5C21A4A620A5278237C70D3E42B* L_3;
		L_3 = XmlNode_get_Document_mF9753FAD0D7BFDBDAE7FEA6B196D7CA2982A373C(L_2, NULL);
		XmlElementListListener_t1513DCDD9C9C5B56690342DCC4445CB4BE6E7E79* L_4 = (XmlElementListListener_t1513DCDD9C9C5B56690342DCC4445CB4BE6E7E79*)il2cpp_codegen_object_new(XmlElementListListener_t1513DCDD9C9C5B56690342DCC4445CB4BE6E7E79_il2cpp_TypeInfo_var);
		XmlElementListListener__ctor_mDCC66BFA14921AA8ADAF5ED16079AB47D603B557(L_4, L_3, __this, NULL);
		WeakReference_tD4B0518CE911FFD9FAAB3FCD492644A354312D8E* L_5 = (WeakReference_tD4B0518CE911FFD9FAAB3FCD492644A354312D8E*)il2cpp_codegen_object_new(WeakReference_tD4B0518CE911FFD9FAAB3FCD492644A354312D8E_il2cpp_TypeInfo_var);
		WeakReference__ctor_m5F9E2F970CD85965A003C0B37ABDBFAA1F5CF241(L_5, L_4, NULL);
		__this->___listener = L_5;
		Il2CppCodeGenWriteBarrier((void**)(&__this->___listener), (void*)L_5);
		return;
	}
}
// Method Definition Index: 93900
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR void XmlElementList_Finalize_mE7104C4073E9CBBC56F2B0428E14B3645798DE93 (XmlElementList_tFE0D5337668FDE708796D7FDB669AE4A290CAB57* __this, const RuntimeMethod* method) 
{
	{
		auto __finallyBlock = il2cpp::utils::Finally([&]
		{

FINALLY_0009:
			{
				Object_Finalize_mC98C96301CCABFE00F1A7EF8E15DF507CACD42B2(__this, NULL);
				return;
			}
		});
		try
		{
			VirtualActionInvoker1< bool >::Invoke(8, __this, (bool)0);
			goto IL_0010;
		}
		catch(Il2CppExceptionWrapper& e)
		{
			__finallyBlock.StoreException(e.ex);
		}
	}

IL_0010:
	{
		return;
	}
}
// Method Definition Index: 93901
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR void XmlElementList_ConcurrencyCheck_mFC4A7DF2FC6B07D06430F714671AB3468E068386 (XmlElementList_tFE0D5337668FDE708796D7FDB669AE4A290CAB57* __this, XmlNodeChangedEventArgs_t965F2E02F8433933D197A0D1AF1EFEEDB524DF3A* ___0_args, const RuntimeMethod* method) 
{
	XmlNameTable_tBDBAACFF3DB40A8E6AF3BDC11F0FF166CF11ABB8* V_0 = NULL;
	{
		bool L_0 = __this->___atomized;
		if (L_0)
		{
			goto IL_0044;
		}
	}
	{
		XmlNode_t3180B9B3D5C36CD58F5327D9F13458E3B3F030AF* L_1 = __this->___rootNode;
		NullCheck(L_1);
		XmlDocument_t4DE82998E642C5C21A4A620A5278237C70D3E42B* L_2;
		L_2 = XmlNode_get_Document_mF9753FAD0D7BFDBDAE7FEA6B196D7CA2982A373C(L_1, NULL);
		NullCheck(L_2);
		XmlNameTable_tBDBAACFF3DB40A8E6AF3BDC11F0FF166CF11ABB8* L_3;
		L_3 = XmlDocument_get_NameTable_m4B913865A24AEA917172F75CBDCE94C81CCB7E2C(L_2, NULL);
		V_0 = L_3;
		XmlNameTable_tBDBAACFF3DB40A8E6AF3BDC11F0FF166CF11ABB8* L_4 = V_0;
		String_t* L_5 = __this->___localName;
		NullCheck(L_4);
		String_t* L_6;
		L_6 = VirtualFuncInvoker1< String_t*, String_t* >::Invoke(4, L_4, L_5);
		__this->___localName = L_6;
		Il2CppCodeGenWriteBarrier((void**)(&__this->___localName), (void*)L_6);
		XmlNameTable_tBDBAACFF3DB40A8E6AF3BDC11F0FF166CF11ABB8* L_7 = V_0;
		String_t* L_8 = __this->___namespaceURI;
		NullCheck(L_7);
		String_t* L_9;
		L_9 = VirtualFuncInvoker1< String_t*, String_t* >::Invoke(4, L_7, L_8);
		__this->___namespaceURI = L_9;
		Il2CppCodeGenWriteBarrier((void**)(&__this->___namespaceURI), (void*)L_9);
		__this->___atomized = (bool)1;
	}

IL_0044:
	{
		XmlNodeChangedEventArgs_t965F2E02F8433933D197A0D1AF1EFEEDB524DF3A* L_10 = ___0_args;
		NullCheck(L_10);
		XmlNode_t3180B9B3D5C36CD58F5327D9F13458E3B3F030AF* L_11;
		L_11 = XmlNodeChangedEventArgs_get_Node_mB5FA92B160E0200CF56DFCBAE3FC40F0D5C9B138_inline(L_10, NULL);
		bool L_12;
		L_12 = XmlElementList_IsMatch_m9459349F92B576BEB725AA24F6337126DC1DE34B(__this, L_11, NULL);
		if (!L_12)
		{
			goto IL_0082;
		}
	}
	{
		int32_t L_13 = __this->___changeCount;
		__this->___changeCount = ((int32_t)il2cpp_codegen_add(L_13, 1));
		__this->___curInd = (-1);
		XmlNode_t3180B9B3D5C36CD58F5327D9F13458E3B3F030AF* L_14 = __this->___rootNode;
		__this->___curElem = L_14;
		Il2CppCodeGenWriteBarrier((void**)(&__this->___curElem), (void*)L_14);
		XmlNodeChangedEventArgs_t965F2E02F8433933D197A0D1AF1EFEEDB524DF3A* L_15 = ___0_args;
		NullCheck(L_15);
		int32_t L_16;
		L_16 = XmlNodeChangedEventArgs_get_Action_mD307B00A56DE5422A4EE4915400EC966DF80BD04_inline(L_15, NULL);
		if (L_16)
		{
			goto IL_0082;
		}
	}
	{
		__this->___empty = (bool)0;
	}

IL_0082:
	{
		__this->___matchCount = (-1);
		return;
	}
}
// Method Definition Index: 93902
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR void XmlElementList__ctor_m38E1065BC153887F600A64F0FAF6D595A928BCB1 (XmlElementList_tFE0D5337668FDE708796D7FDB669AE4A290CAB57* __this, XmlNode_t3180B9B3D5C36CD58F5327D9F13458E3B3F030AF* ___0_parent, String_t* ___1_name, const RuntimeMethod* method) 
{
	static bool s_Il2CppMethodInitialized;
	if (!s_Il2CppMethodInitialized)
	{
		il2cpp_codegen_initialize_runtime_metadata((uintptr_t*)&_stringLiteralE280D065A824A791F8305234D3E093FC9A5A90C7);
		s_Il2CppMethodInitialized = true;
	}
	XmlNameTable_tBDBAACFF3DB40A8E6AF3BDC11F0FF166CF11ABB8* V_0 = NULL;
	{
		XmlNode_t3180B9B3D5C36CD58F5327D9F13458E3B3F030AF* L_0 = ___0_parent;
		XmlElementList__ctor_mE9CE5108CF2B91213733B81C26C40C57D5726B07(__this, L_0, NULL);
		XmlNode_t3180B9B3D5C36CD58F5327D9F13458E3B3F030AF* L_1 = ___0_parent;
		NullCheck(L_1);
		XmlDocument_t4DE82998E642C5C21A4A620A5278237C70D3E42B* L_2;
		L_2 = XmlNode_get_Document_mF9753FAD0D7BFDBDAE7FEA6B196D7CA2982A373C(L_1, NULL);
		NullCheck(L_2);
		XmlNameTable_tBDBAACFF3DB40A8E6AF3BDC11F0FF166CF11ABB8* L_3;
		L_3 = XmlDocument_get_NameTable_m4B913865A24AEA917172F75CBDCE94C81CCB7E2C(L_2, NULL);
		V_0 = L_3;
		XmlNameTable_tBDBAACFF3DB40A8E6AF3BDC11F0FF166CF11ABB8* L_4 = V_0;
		NullCheck(L_4);
		String_t* L_5;
		L_5 = VirtualFuncInvoker1< String_t*, String_t* >::Invoke(4, L_4, _stringLiteralE280D065A824A791F8305234D3E093FC9A5A90C7);
		__this->___asterisk = L_5;
		Il2CppCodeGenWriteBarrier((void**)(&__this->___asterisk), (void*)L_5);
		XmlNameTable_tBDBAACFF3DB40A8E6AF3BDC11F0FF166CF11ABB8* L_6 = V_0;
		String_t* L_7 = ___1_name;
		NullCheck(L_6);
		String_t* L_8;
		L_8 = VirtualFuncInvoker1< String_t*, String_t* >::Invoke(4, L_6, L_7);
		__this->___name = L_8;
		Il2CppCodeGenWriteBarrier((void**)(&__this->___name), (void*)L_8);
		__this->___localName = (String_t*)NULL;
		Il2CppCodeGenWriteBarrier((void**)(&__this->___localName), (void*)(String_t*)NULL);
		__this->___namespaceURI = (String_t*)NULL;
		Il2CppCodeGenWriteBarrier((void**)(&__this->___namespaceURI), (void*)(String_t*)NULL);
		return;
	}
}
// Method Definition Index: 93903
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR int32_t XmlElementList_get_ChangeCount_m6C653A302721EA1D8C41326E169F2ED54CD9EE34 (XmlElementList_tFE0D5337668FDE708796D7FDB669AE4A290CAB57* __this, const RuntimeMethod* method) 
{
	{
		int32_t L_0 = __this->___changeCount;
		return L_0;
	}
}
// Method Definition Index: 93904
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR XmlNode_t3180B9B3D5C36CD58F5327D9F13458E3B3F030AF* XmlElementList_NextElemInPreOrder_m6AA47AD12DFF62032C6A09B5EC1879631E0E0A6C (XmlElementList_tFE0D5337668FDE708796D7FDB669AE4A290CAB57* __this, XmlNode_t3180B9B3D5C36CD58F5327D9F13458E3B3F030AF* ___0_curNode, const RuntimeMethod* method) 
{
	XmlNode_t3180B9B3D5C36CD58F5327D9F13458E3B3F030AF* V_0 = NULL;
	{
		XmlNode_t3180B9B3D5C36CD58F5327D9F13458E3B3F030AF* L_0 = ___0_curNode;
		NullCheck(L_0);
		XmlNode_t3180B9B3D5C36CD58F5327D9F13458E3B3F030AF* L_1;
		L_1 = VirtualFuncInvoker0< XmlNode_t3180B9B3D5C36CD58F5327D9F13458E3B3F030AF* >::Invoke(13, L_0);
		V_0 = L_1;
		XmlNode_t3180B9B3D5C36CD58F5327D9F13458E3B3F030AF* L_2 = V_0;
		if (L_2)
		{
			goto IL_003c;
		}
	}
	{
		XmlNode_t3180B9B3D5C36CD58F5327D9F13458E3B3F030AF* L_3 = ___0_curNode;
		V_0 = L_3;
		goto IL_0015;
	}

IL_000e:
	{
		XmlNode_t3180B9B3D5C36CD58F5327D9F13458E3B3F030AF* L_4 = V_0;
		NullCheck(L_4);
		XmlNode_t3180B9B3D5C36CD58F5327D9F13458E3B3F030AF* L_5;
		L_5 = VirtualFuncInvoker0< XmlNode_t3180B9B3D5C36CD58F5327D9F13458E3B3F030AF* >::Invoke(7, L_4);
		V_0 = L_5;
	}

IL_0015:
	{
		XmlNode_t3180B9B3D5C36CD58F5327D9F13458E3B3F030AF* L_6 = V_0;
		if (!L_6)
		{
			goto IL_0029;
		}
	}
	{
		XmlNode_t3180B9B3D5C36CD58F5327D9F13458E3B3F030AF* L_7 = V_0;
		XmlNode_t3180B9B3D5C36CD58F5327D9F13458E3B3F030AF* L_8 = __this->___rootNode;
		if ((((RuntimeObject*)(XmlNode_t3180B9B3D5C36CD58F5327D9F13458E3B3F030AF*)L_7) == ((RuntimeObject*)(XmlNode_t3180B9B3D5C36CD58F5327D9F13458E3B3F030AF*)L_8)))
		{
			goto IL_0029;
		}
	}
	{
		XmlNode_t3180B9B3D5C36CD58F5327D9F13458E3B3F030AF* L_9 = V_0;
		NullCheck(L_9);
		XmlNode_t3180B9B3D5C36CD58F5327D9F13458E3B3F030AF* L_10;
		L_10 = VirtualFuncInvoker0< XmlNode_t3180B9B3D5C36CD58F5327D9F13458E3B3F030AF* >::Invoke(10, L_9);
		if (!L_10)
		{
			goto IL_000e;
		}
	}

IL_0029:
	{
		XmlNode_t3180B9B3D5C36CD58F5327D9F13458E3B3F030AF* L_11 = V_0;
		if (!L_11)
		{
			goto IL_003c;
		}
	}
	{
		XmlNode_t3180B9B3D5C36CD58F5327D9F13458E3B3F030AF* L_12 = V_0;
		XmlNode_t3180B9B3D5C36CD58F5327D9F13458E3B3F030AF* L_13 = __this->___rootNode;
		if ((((RuntimeObject*)(XmlNode_t3180B9B3D5C36CD58F5327D9F13458E3B3F030AF*)L_12) == ((RuntimeObject*)(XmlNode_t3180B9B3D5C36CD58F5327D9F13458E3B3F030AF*)L_13)))
		{
			goto IL_003c;
		}
	}
	{
		XmlNode_t3180B9B3D5C36CD58F5327D9F13458E3B3F030AF* L_14 = V_0;
		NullCheck(L_14);
		XmlNode_t3180B9B3D5C36CD58F5327D9F13458E3B3F030AF* L_15;
		L_15 = VirtualFuncInvoker0< XmlNode_t3180B9B3D5C36CD58F5327D9F13458E3B3F030AF* >::Invoke(10, L_14);
		V_0 = L_15;
	}

IL_003c:
	{
		XmlNode_t3180B9B3D5C36CD58F5327D9F13458E3B3F030AF* L_16 = V_0;
		XmlNode_t3180B9B3D5C36CD58F5327D9F13458E3B3F030AF* L_17 = __this->___rootNode;
		if ((!(((RuntimeObject*)(XmlNode_t3180B9B3D5C36CD58F5327D9F13458E3B3F030AF*)L_16) == ((RuntimeObject*)(XmlNode_t3180B9B3D5C36CD58F5327D9F13458E3B3F030AF*)L_17))))
		{
			goto IL_0047;
		}
	}
	{
		V_0 = (XmlNode_t3180B9B3D5C36CD58F5327D9F13458E3B3F030AF*)NULL;
	}

IL_0047:
	{
		XmlNode_t3180B9B3D5C36CD58F5327D9F13458E3B3F030AF* L_18 = V_0;
		return L_18;
	}
}
// Method Definition Index: 93905
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR XmlNode_t3180B9B3D5C36CD58F5327D9F13458E3B3F030AF* XmlElementList_PrevElemInPreOrder_m3DE0B0989925E78696BA5CAA68854052E911EE2B (XmlElementList_tFE0D5337668FDE708796D7FDB669AE4A290CAB57* __this, XmlNode_t3180B9B3D5C36CD58F5327D9F13458E3B3F030AF* ___0_curNode, const RuntimeMethod* method) 
{
	XmlNode_t3180B9B3D5C36CD58F5327D9F13458E3B3F030AF* V_0 = NULL;
	{
		XmlNode_t3180B9B3D5C36CD58F5327D9F13458E3B3F030AF* L_0 = ___0_curNode;
		NullCheck(L_0);
		XmlNode_t3180B9B3D5C36CD58F5327D9F13458E3B3F030AF* L_1;
		L_1 = VirtualFuncInvoker0< XmlNode_t3180B9B3D5C36CD58F5327D9F13458E3B3F030AF* >::Invoke(9, L_0);
		V_0 = L_1;
		goto IL_0018;
	}

IL_0009:
	{
		XmlNode_t3180B9B3D5C36CD58F5327D9F13458E3B3F030AF* L_2 = V_0;
		NullCheck(L_2);
		XmlNode_t3180B9B3D5C36CD58F5327D9F13458E3B3F030AF* L_3;
		L_3 = VirtualFuncInvoker0< XmlNode_t3180B9B3D5C36CD58F5327D9F13458E3B3F030AF* >::Invoke(14, L_2);
		if (!L_3)
		{
			goto IL_001b;
		}
	}
	{
		XmlNode_t3180B9B3D5C36CD58F5327D9F13458E3B3F030AF* L_4 = V_0;
		NullCheck(L_4);
		XmlNode_t3180B9B3D5C36CD58F5327D9F13458E3B3F030AF* L_5;
		L_5 = VirtualFuncInvoker0< XmlNode_t3180B9B3D5C36CD58F5327D9F13458E3B3F030AF* >::Invoke(14, L_4);
		V_0 = L_5;
	}

IL_0018:
	{
		XmlNode_t3180B9B3D5C36CD58F5327D9F13458E3B3F030AF* L_6 = V_0;
		if (L_6)
		{
			goto IL_0009;
		}
	}

IL_001b:
	{
		XmlNode_t3180B9B3D5C36CD58F5327D9F13458E3B3F030AF* L_7 = V_0;
		if (L_7)
		{
			goto IL_0025;
		}
	}
	{
		XmlNode_t3180B9B3D5C36CD58F5327D9F13458E3B3F030AF* L_8 = ___0_curNode;
		NullCheck(L_8);
		XmlNode_t3180B9B3D5C36CD58F5327D9F13458E3B3F030AF* L_9;
		L_9 = VirtualFuncInvoker0< XmlNode_t3180B9B3D5C36CD58F5327D9F13458E3B3F030AF* >::Invoke(7, L_8);
		V_0 = L_9;
	}

IL_0025:
	{
		XmlNode_t3180B9B3D5C36CD58F5327D9F13458E3B3F030AF* L_10 = V_0;
		XmlNode_t3180B9B3D5C36CD58F5327D9F13458E3B3F030AF* L_11 = __this->___rootNode;
		if ((!(((RuntimeObject*)(XmlNode_t3180B9B3D5C36CD58F5327D9F13458E3B3F030AF*)L_10) == ((RuntimeObject*)(XmlNode_t3180B9B3D5C36CD58F5327D9F13458E3B3F030AF*)L_11))))
		{
			goto IL_0030;
		}
	}
	{
		V_0 = (XmlNode_t3180B9B3D5C36CD58F5327D9F13458E3B3F030AF*)NULL;
	}

IL_0030:
	{
		XmlNode_t3180B9B3D5C36CD58F5327D9F13458E3B3F030AF* L_12 = V_0;
		return L_12;
	}
}
// Method Definition Index: 93906
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR bool XmlElementList_IsMatch_m9459349F92B576BEB725AA24F6337126DC1DE34B (XmlElementList_tFE0D5337668FDE708796D7FDB669AE4A290CAB57* __this, XmlNode_t3180B9B3D5C36CD58F5327D9F13458E3B3F030AF* ___0_curNode, const RuntimeMethod* method) 
{
	{
		XmlNode_t3180B9B3D5C36CD58F5327D9F13458E3B3F030AF* L_0 = ___0_curNode;
		NullCheck(L_0);
		int32_t L_1;
		L_1 = VirtualFuncInvoker0< int32_t >::Invoke(6, L_0);
		if ((!(((uint32_t)L_1) == ((uint32_t)1))))
		{
			goto IL_0087;
		}
	}
	{
		String_t* L_2 = __this->___name;
		if (!L_2)
		{
			goto IL_0039;
		}
	}
	{
		String_t* L_3 = __this->___name;
		String_t* L_4 = __this->___asterisk;
		bool L_5;
		L_5 = Ref_Equal_m60FD3EF02546279815B785F856568B01F60FE1AF(L_3, L_4, NULL);
		if (L_5)
		{
			goto IL_0037;
		}
	}
	{
		XmlNode_t3180B9B3D5C36CD58F5327D9F13458E3B3F030AF* L_6 = ___0_curNode;
		NullCheck(L_6);
		String_t* L_7;
		L_7 = VirtualFuncInvoker0< String_t* >::Invoke(4, L_6);
		String_t* L_8 = __this->___name;
		bool L_9;
		L_9 = Ref_Equal_m60FD3EF02546279815B785F856568B01F60FE1AF(L_7, L_8, NULL);
		if (!L_9)
		{
			goto IL_0087;
		}
	}

IL_0037:
	{
		return (bool)1;
	}

IL_0039:
	{
		String_t* L_10 = __this->___localName;
		String_t* L_11 = __this->___asterisk;
		bool L_12;
		L_12 = Ref_Equal_m60FD3EF02546279815B785F856568B01F60FE1AF(L_10, L_11, NULL);
		if (L_12)
		{
			goto IL_005f;
		}
	}
	{
		XmlNode_t3180B9B3D5C36CD58F5327D9F13458E3B3F030AF* L_13 = ___0_curNode;
		NullCheck(L_13);
		String_t* L_14;
		L_14 = VirtualFuncInvoker0< String_t* >::Invoke(17, L_13);
		String_t* L_15 = __this->___localName;
		bool L_16;
		L_16 = Ref_Equal_m60FD3EF02546279815B785F856568B01F60FE1AF(L_14, L_15, NULL);
		if (!L_16)
		{
			goto IL_0087;
		}
	}

IL_005f:
	{
		String_t* L_17 = __this->___namespaceURI;
		String_t* L_18 = __this->___asterisk;
		bool L_19;
		L_19 = Ref_Equal_m60FD3EF02546279815B785F856568B01F60FE1AF(L_17, L_18, NULL);
		if (L_19)
		{
			goto IL_0085;
		}
	}
	{
		XmlNode_t3180B9B3D5C36CD58F5327D9F13458E3B3F030AF* L_20 = ___0_curNode;
		NullCheck(L_20);
		String_t* L_21;
		L_21 = VirtualFuncInvoker0< String_t* >::Invoke(16, L_20);
		String_t* L_22 = __this->___namespaceURI;
		bool L_23;
		L_23 = String_op_Equality_m030E1B219352228970A076136E455C4E568C02C1(L_21, L_22, NULL);
		if (!L_23)
		{
			goto IL_0087;
		}
	}

IL_0085:
	{
		return (bool)1;
	}

IL_0087:
	{
		return (bool)0;
	}
}
// Method Definition Index: 93907
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR XmlNode_t3180B9B3D5C36CD58F5327D9F13458E3B3F030AF* XmlElementList_GetMatchingNode_m2CC1EB6A57B17D3B4A8FC7B16226F8ED1E3EC137 (XmlElementList_tFE0D5337668FDE708796D7FDB669AE4A290CAB57* __this, XmlNode_t3180B9B3D5C36CD58F5327D9F13458E3B3F030AF* ___0_n, bool ___1_bNext, const RuntimeMethod* method) 
{
	XmlNode_t3180B9B3D5C36CD58F5327D9F13458E3B3F030AF* V_0 = NULL;
	{
		XmlNode_t3180B9B3D5C36CD58F5327D9F13458E3B3F030AF* L_0 = ___0_n;
		V_0 = L_0;
	}

IL_0002:
	{
		bool L_1 = ___1_bNext;
		if (!L_1)
		{
			goto IL_000f;
		}
	}
	{
		XmlNode_t3180B9B3D5C36CD58F5327D9F13458E3B3F030AF* L_2 = V_0;
		XmlNode_t3180B9B3D5C36CD58F5327D9F13458E3B3F030AF* L_3;
		L_3 = XmlElementList_NextElemInPreOrder_m6AA47AD12DFF62032C6A09B5EC1879631E0E0A6C(__this, L_2, NULL);
		V_0 = L_3;
		goto IL_0017;
	}

IL_000f:
	{
		XmlNode_t3180B9B3D5C36CD58F5327D9F13458E3B3F030AF* L_4 = V_0;
		XmlNode_t3180B9B3D5C36CD58F5327D9F13458E3B3F030AF* L_5;
		L_5 = XmlElementList_PrevElemInPreOrder_m3DE0B0989925E78696BA5CAA68854052E911EE2B(__this, L_4, NULL);
		V_0 = L_5;
	}

IL_0017:
	{
		XmlNode_t3180B9B3D5C36CD58F5327D9F13458E3B3F030AF* L_6 = V_0;
		if (!L_6)
		{
			goto IL_0023;
		}
	}
	{
		XmlNode_t3180B9B3D5C36CD58F5327D9F13458E3B3F030AF* L_7 = V_0;
		bool L_8;
		L_8 = XmlElementList_IsMatch_m9459349F92B576BEB725AA24F6337126DC1DE34B(__this, L_7, NULL);
		if (!L_8)
		{
			goto IL_0002;
		}
	}

IL_0023:
	{
		XmlNode_t3180B9B3D5C36CD58F5327D9F13458E3B3F030AF* L_9 = V_0;
		return L_9;
	}
}
// Method Definition Index: 93908
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR XmlNode_t3180B9B3D5C36CD58F5327D9F13458E3B3F030AF* XmlElementList_GetNextNode_m54C6F8839217646D19DF43C2AB3FFE87ECABF04B (XmlElementList_tFE0D5337668FDE708796D7FDB669AE4A290CAB57* __this, XmlNode_t3180B9B3D5C36CD58F5327D9F13458E3B3F030AF* ___0_n, const RuntimeMethod* method) 
{
	XmlNode_t3180B9B3D5C36CD58F5327D9F13458E3B3F030AF* V_0 = NULL;
	XmlNode_t3180B9B3D5C36CD58F5327D9F13458E3B3F030AF* G_B5_0 = NULL;
	{
		bool L_0 = __this->___empty;
		if (!L_0)
		{
			goto IL_000a;
		}
	}
	{
		return (XmlNode_t3180B9B3D5C36CD58F5327D9F13458E3B3F030AF*)NULL;
	}

IL_000a:
	{
		XmlNode_t3180B9B3D5C36CD58F5327D9F13458E3B3F030AF* L_1 = ___0_n;
		if (!L_1)
		{
			goto IL_0010;
		}
	}
	{
		XmlNode_t3180B9B3D5C36CD58F5327D9F13458E3B3F030AF* L_2 = ___0_n;
		G_B5_0 = L_2;
		goto IL_0016;
	}

IL_0010:
	{
		XmlNode_t3180B9B3D5C36CD58F5327D9F13458E3B3F030AF* L_3 = __this->___rootNode;
		G_B5_0 = L_3;
	}

IL_0016:
	{
		V_0 = G_B5_0;
		XmlNode_t3180B9B3D5C36CD58F5327D9F13458E3B3F030AF* L_4 = V_0;
		XmlNode_t3180B9B3D5C36CD58F5327D9F13458E3B3F030AF* L_5;
		L_5 = XmlElementList_GetMatchingNode_m2CC1EB6A57B17D3B4A8FC7B16226F8ED1E3EC137(__this, L_4, (bool)1, NULL);
		return L_5;
	}
}
// Method Definition Index: 93909
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR RuntimeObject* XmlElementList_GetEnumerator_m9C65429533CBB5B262B9CBF50F87376187D99BCE (XmlElementList_tFE0D5337668FDE708796D7FDB669AE4A290CAB57* __this, const RuntimeMethod* method) 
{
	static bool s_Il2CppMethodInitialized;
	if (!s_Il2CppMethodInitialized)
	{
		il2cpp_codegen_initialize_runtime_metadata((uintptr_t*)&XmlElementListEnumerator_tFF3D3868A2EFB7D39A510085383C371172943794_il2cpp_TypeInfo_var);
		il2cpp_codegen_initialize_runtime_metadata((uintptr_t*)&XmlEmptyElementListEnumerator_tBA7DE21795A44BB9D31419C2CBB40A83D6DBEE84_il2cpp_TypeInfo_var);
		s_Il2CppMethodInitialized = true;
	}
	{
		bool L_0 = __this->___empty;
		if (!L_0)
		{
			goto IL_000f;
		}
	}
	{
		XmlEmptyElementListEnumerator_tBA7DE21795A44BB9D31419C2CBB40A83D6DBEE84* L_1 = (XmlEmptyElementListEnumerator_tBA7DE21795A44BB9D31419C2CBB40A83D6DBEE84*)il2cpp_codegen_object_new(XmlEmptyElementListEnumerator_tBA7DE21795A44BB9D31419C2CBB40A83D6DBEE84_il2cpp_TypeInfo_var);
		XmlEmptyElementListEnumerator__ctor_m7433D2C2DDEF90DD50C7AF1816454F41949D5314(L_1, __this, NULL);
		return L_1;
	}

IL_000f:
	{
		XmlElementListEnumerator_tFF3D3868A2EFB7D39A510085383C371172943794* L_2 = (XmlElementListEnumerator_tFF3D3868A2EFB7D39A510085383C371172943794*)il2cpp_codegen_object_new(XmlElementListEnumerator_tFF3D3868A2EFB7D39A510085383C371172943794_il2cpp_TypeInfo_var);
		XmlElementListEnumerator__ctor_m98B779C6190C647319FDF934EEC2C9DC22B63204(L_2, __this, NULL);
		return L_2;
	}
}
// Method Definition Index: 93910
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR void XmlElementList_PrivateDisposeNodeList_m91F3D67969060E933FD09BA62FD222477C04BFAB (XmlElementList_tFE0D5337668FDE708796D7FDB669AE4A290CAB57* __this, const RuntimeMethod* method) 
{
	static bool s_Il2CppMethodInitialized;
	if (!s_Il2CppMethodInitialized)
	{
		il2cpp_codegen_initialize_runtime_metadata((uintptr_t*)&GC_t920F9CF6EBB7C787E5010A4352E1B587F356DC58_il2cpp_TypeInfo_var);
		s_Il2CppMethodInitialized = true;
	}
	{
		il2cpp_codegen_runtime_class_init_inline(GC_t920F9CF6EBB7C787E5010A4352E1B587F356DC58_il2cpp_TypeInfo_var);
		GC_SuppressFinalize_m71815DBD5A0CD2EA1BE43317B08B7A14949EDC65(__this, NULL);
		VirtualActionInvoker1< bool >::Invoke(8, __this, (bool)1);
		return;
	}
}
// Method Definition Index: 93911
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR void XmlElementList_Dispose_mE31A421BA8D482AEE3F8FE4F0CEAFF6B03E105D3 (XmlElementList_tFE0D5337668FDE708796D7FDB669AE4A290CAB57* __this, bool ___0_disposing, const RuntimeMethod* method) 
{
	static bool s_Il2CppMethodInitialized;
	if (!s_Il2CppMethodInitialized)
	{
		il2cpp_codegen_initialize_runtime_metadata((uintptr_t*)&XmlElementListListener_t1513DCDD9C9C5B56690342DCC4445CB4BE6E7E79_il2cpp_TypeInfo_var);
		s_Il2CppMethodInitialized = true;
	}
	XmlElementListListener_t1513DCDD9C9C5B56690342DCC4445CB4BE6E7E79* V_0 = NULL;
	{
		WeakReference_tD4B0518CE911FFD9FAAB3FCD492644A354312D8E* L_0 = __this->___listener;
		if (!L_0)
		{
			goto IL_0029;
		}
	}
	{
		WeakReference_tD4B0518CE911FFD9FAAB3FCD492644A354312D8E* L_1 = __this->___listener;
		NullCheck(L_1);
		RuntimeObject* L_2;
		L_2 = VirtualFuncInvoker0< RuntimeObject* >::Invoke(6, L_1);
		V_0 = ((XmlElementListListener_t1513DCDD9C9C5B56690342DCC4445CB4BE6E7E79*)CastclassClass((RuntimeObject*)L_2, XmlElementListListener_t1513DCDD9C9C5B56690342DCC4445CB4BE6E7E79_il2cpp_TypeInfo_var));
		XmlElementListListener_t1513DCDD9C9C5B56690342DCC4445CB4BE6E7E79* L_3 = V_0;
		if (!L_3)
		{
			goto IL_0022;
		}
	}
	{
		XmlElementListListener_t1513DCDD9C9C5B56690342DCC4445CB4BE6E7E79* L_4 = V_0;
		NullCheck(L_4);
		XmlElementListListener_Unregister_m200394882802F40AE1337CE7EEC60ACAA606287C(L_4, NULL);
	}

IL_0022:
	{
		__this->___listener = (WeakReference_tD4B0518CE911FFD9FAAB3FCD492644A354312D8E*)NULL;
		Il2CppCodeGenWriteBarrier((void**)(&__this->___listener), (void*)(WeakReference_tD4B0518CE911FFD9FAAB3FCD492644A354312D8E*)NULL);
	}

IL_0029:
	{
		return;
	}
}
#ifdef __clang__
#pragma clang diagnostic pop
#endif
#ifdef __clang__
#pragma clang diagnostic push
#pragma clang diagnostic ignored "-Winvalid-offsetof"
#pragma clang diagnostic ignored "-Wunused-variable"
#endif
// Method Definition Index: 93912
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR void XmlElementListEnumerator__ctor_m98B779C6190C647319FDF934EEC2C9DC22B63204 (XmlElementListEnumerator_tFF3D3868A2EFB7D39A510085383C371172943794* __this, XmlElementList_tFE0D5337668FDE708796D7FDB669AE4A290CAB57* ___0_list, const RuntimeMethod* method) 
{
	{
		Object__ctor_mE837C6B9FA8C6D5D109F4B2EC885D79919AC0EA2(__this, NULL);
		XmlElementList_tFE0D5337668FDE708796D7FDB669AE4A290CAB57* L_0 = ___0_list;
		__this->___list = L_0;
		Il2CppCodeGenWriteBarrier((void**)(&__this->___list), (void*)L_0);
		__this->___curElem = (XmlNode_t3180B9B3D5C36CD58F5327D9F13458E3B3F030AF*)NULL;
		Il2CppCodeGenWriteBarrier((void**)(&__this->___curElem), (void*)(XmlNode_t3180B9B3D5C36CD58F5327D9F13458E3B3F030AF*)NULL);
		XmlElementList_tFE0D5337668FDE708796D7FDB669AE4A290CAB57* L_1 = ___0_list;
		NullCheck(L_1);
		int32_t L_2;
		L_2 = XmlElementList_get_ChangeCount_m6C653A302721EA1D8C41326E169F2ED54CD9EE34_inline(L_1, NULL);
		__this->___changeCount = L_2;
		return;
	}
}
// Method Definition Index: 93913
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR bool XmlElementListEnumerator_MoveNext_mAA954F4C42CDB8AF1602675835099FD6B5162ED1 (XmlElementListEnumerator_tFF3D3868A2EFB7D39A510085383C371172943794* __this, const RuntimeMethod* method) 
{
	{
		XmlElementList_tFE0D5337668FDE708796D7FDB669AE4A290CAB57* L_0 = __this->___list;
		NullCheck(L_0);
		int32_t L_1;
		L_1 = XmlElementList_get_ChangeCount_m6C653A302721EA1D8C41326E169F2ED54CD9EE34_inline(L_0, NULL);
		int32_t L_2 = __this->___changeCount;
		if ((((int32_t)L_1) == ((int32_t)L_2)))
		{
			goto IL_0023;
		}
	}
	{
		String_t* L_3;
		L_3 = Res_GetString_mBEE82ACFE97F93A6DDE81E322477CF2713466364(((String_t*)il2cpp_codegen_initialize_runtime_metadata_inline((uintptr_t*)&_stringLiteral2FF4A9E93E0170BF78F5B084F76C8B3194FAE523)), NULL);
		InvalidOperationException_t5DDE4D49B7405FAAB1E4576F4715A42A3FAD4BAB* L_4 = (InvalidOperationException_t5DDE4D49B7405FAAB1E4576F4715A42A3FAD4BAB*)il2cpp_codegen_object_new(((RuntimeClass*)il2cpp_codegen_initialize_runtime_metadata_inline((uintptr_t*)&InvalidOperationException_t5DDE4D49B7405FAAB1E4576F4715A42A3FAD4BAB_il2cpp_TypeInfo_var)));
		InvalidOperationException__ctor_mE4CB6F4712AB6D99A2358FBAE2E052B3EE976162(L_4, L_3, NULL);
		IL2CPP_RAISE_MANAGED_EXCEPTION(L_4, ((RuntimeMethod*)il2cpp_codegen_initialize_runtime_metadata_inline((uintptr_t*)&XmlElementListEnumerator_MoveNext_mAA954F4C42CDB8AF1602675835099FD6B5162ED1_RuntimeMethod_var)));
	}

IL_0023:
	{
		XmlElementList_tFE0D5337668FDE708796D7FDB669AE4A290CAB57* L_5 = __this->___list;
		XmlNode_t3180B9B3D5C36CD58F5327D9F13458E3B3F030AF* L_6 = __this->___curElem;
		NullCheck(L_5);
		XmlNode_t3180B9B3D5C36CD58F5327D9F13458E3B3F030AF* L_7;
		L_7 = XmlElementList_GetNextNode_m54C6F8839217646D19DF43C2AB3FFE87ECABF04B(L_5, L_6, NULL);
		__this->___curElem = L_7;
		Il2CppCodeGenWriteBarrier((void**)(&__this->___curElem), (void*)L_7);
		XmlNode_t3180B9B3D5C36CD58F5327D9F13458E3B3F030AF* L_8 = __this->___curElem;
		return (bool)((!(((RuntimeObject*)(XmlNode_t3180B9B3D5C36CD58F5327D9F13458E3B3F030AF*)L_8) <= ((RuntimeObject*)(RuntimeObject*)NULL)))? 1 : 0);
	}
}
// Method Definition Index: 93914
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR void XmlElementListEnumerator_Reset_mA13C38F5A39A5926F4CA0CAAF25288482366B876 (XmlElementListEnumerator_tFF3D3868A2EFB7D39A510085383C371172943794* __this, const RuntimeMethod* method) 
{
	{
		__this->___curElem = (XmlNode_t3180B9B3D5C36CD58F5327D9F13458E3B3F030AF*)NULL;
		Il2CppCodeGenWriteBarrier((void**)(&__this->___curElem), (void*)(XmlNode_t3180B9B3D5C36CD58F5327D9F13458E3B3F030AF*)NULL);
		XmlElementList_tFE0D5337668FDE708796D7FDB669AE4A290CAB57* L_0 = __this->___list;
		NullCheck(L_0);
		int32_t L_1;
		L_1 = XmlElementList_get_ChangeCount_m6C653A302721EA1D8C41326E169F2ED54CD9EE34_inline(L_0, NULL);
		__this->___changeCount = L_1;
		return;
	}
}
// Method Definition Index: 93915
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR RuntimeObject* XmlElementListEnumerator_get_Current_mA75BD3576C64F7039DD8332157BEE7683841D079 (XmlElementListEnumerator_tFF3D3868A2EFB7D39A510085383C371172943794* __this, const RuntimeMethod* method) 
{
	{
		XmlNode_t3180B9B3D5C36CD58F5327D9F13458E3B3F030AF* L_0 = __this->___curElem;
		return L_0;
	}
}
#ifdef __clang__
#pragma clang diagnostic pop
#endif
#ifdef __clang__
#pragma clang diagnostic push
#pragma clang diagnostic ignored "-Winvalid-offsetof"
#pragma clang diagnostic ignored "-Wunused-variable"
#endif
// Method Definition Index: 93916
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR void XmlEmptyElementListEnumerator__ctor_m7433D2C2DDEF90DD50C7AF1816454F41949D5314 (XmlEmptyElementListEnumerator_tBA7DE21795A44BB9D31419C2CBB40A83D6DBEE84* __this, XmlElementList_tFE0D5337668FDE708796D7FDB669AE4A290CAB57* ___0_list, const RuntimeMethod* method) 
{
	{
		Object__ctor_mE837C6B9FA8C6D5D109F4B2EC885D79919AC0EA2(__this, NULL);
		return;
	}
}
// Method Definition Index: 93917
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR bool XmlEmptyElementListEnumerator_MoveNext_m66678E54A5EC0906958E81FE3BD490AEB7613109 (XmlEmptyElementListEnumerator_tBA7DE21795A44BB9D31419C2CBB40A83D6DBEE84* __this, const RuntimeMethod* method) 
{
	{
		return (bool)0;
	}
}
// Method Definition Index: 93918
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR void XmlEmptyElementListEnumerator_Reset_mFB303DE624E20111A7B8797137A84907EE82A7A4 (XmlEmptyElementListEnumerator_tBA7DE21795A44BB9D31419C2CBB40A83D6DBEE84* __this, const RuntimeMethod* method) 
{
	{
		return;
	}
}
// Method Definition Index: 93919
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR RuntimeObject* XmlEmptyElementListEnumerator_get_Current_mA23B6E85AD48001156124865EA9D5C5BA94AB51F (XmlEmptyElementListEnumerator_tBA7DE21795A44BB9D31419C2CBB40A83D6DBEE84* __this, const RuntimeMethod* method) 
{
	{
		return NULL;
	}
}
#ifdef __clang__
#pragma clang diagnostic pop
#endif
#ifdef __clang__
#pragma clang diagnostic push
#pragma clang diagnostic ignored "-Winvalid-offsetof"
#pragma clang diagnostic ignored "-Wunused-variable"
#endif
// Method Definition Index: 93920
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR void XmlElementListListener__ctor_mDCC66BFA14921AA8ADAF5ED16079AB47D603B557 (XmlElementListListener_t1513DCDD9C9C5B56690342DCC4445CB4BE6E7E79* __this, XmlDocument_t4DE82998E642C5C21A4A620A5278237C70D3E42B* ___0_doc, XmlElementList_tFE0D5337668FDE708796D7FDB669AE4A290CAB57* ___1_elemList, const RuntimeMethod* method) 
{
	static bool s_Il2CppMethodInitialized;
	if (!s_Il2CppMethodInitialized)
	{
		il2cpp_codegen_initialize_runtime_metadata((uintptr_t*)&WeakReference_tD4B0518CE911FFD9FAAB3FCD492644A354312D8E_il2cpp_TypeInfo_var);
		il2cpp_codegen_initialize_runtime_metadata((uintptr_t*)&XmlElementListListener_OnListChanged_mD4D9B710020444018B7CA3A06C295978B38943DD_RuntimeMethod_var);
		il2cpp_codegen_initialize_runtime_metadata((uintptr_t*)&XmlNodeChangedEventHandler_t907F6E9CB8DE238635028EC468AD41CAB0BA269B_il2cpp_TypeInfo_var);
		s_Il2CppMethodInitialized = true;
	}
	{
		Object__ctor_mE837C6B9FA8C6D5D109F4B2EC885D79919AC0EA2(__this, NULL);
		XmlDocument_t4DE82998E642C5C21A4A620A5278237C70D3E42B* L_0 = ___0_doc;
		__this->___doc = L_0;
		Il2CppCodeGenWriteBarrier((void**)(&__this->___doc), (void*)L_0);
		XmlElementList_tFE0D5337668FDE708796D7FDB669AE4A290CAB57* L_1 = ___1_elemList;
		WeakReference_tD4B0518CE911FFD9FAAB3FCD492644A354312D8E* L_2 = (WeakReference_tD4B0518CE911FFD9FAAB3FCD492644A354312D8E*)il2cpp_codegen_object_new(WeakReference_tD4B0518CE911FFD9FAAB3FCD492644A354312D8E_il2cpp_TypeInfo_var);
		WeakReference__ctor_m5F9E2F970CD85965A003C0B37ABDBFAA1F5CF241(L_2, L_1, NULL);
		__this->___elemList = L_2;
		Il2CppCodeGenWriteBarrier((void**)(&__this->___elemList), (void*)L_2);
		XmlNodeChangedEventHandler_t907F6E9CB8DE238635028EC468AD41CAB0BA269B* L_3 = (XmlNodeChangedEventHandler_t907F6E9CB8DE238635028EC468AD41CAB0BA269B*)il2cpp_codegen_object_new(XmlNodeChangedEventHandler_t907F6E9CB8DE238635028EC468AD41CAB0BA269B_il2cpp_TypeInfo_var);
		XmlNodeChangedEventHandler__ctor_mD51AE5833F8A9B1EA9DB8DC54FEF3AD64763AED9(L_3, __this, (intptr_t)((void*)XmlElementListListener_OnListChanged_mD4D9B710020444018B7CA3A06C295978B38943DD_RuntimeMethod_var), NULL);
		__this->___nodeChangeHandler = L_3;
		Il2CppCodeGenWriteBarrier((void**)(&__this->___nodeChangeHandler), (void*)L_3);
		XmlDocument_t4DE82998E642C5C21A4A620A5278237C70D3E42B* L_4 = ___0_doc;
		XmlNodeChangedEventHandler_t907F6E9CB8DE238635028EC468AD41CAB0BA269B* L_5 = __this->___nodeChangeHandler;
		NullCheck(L_4);
		XmlDocument_add_NodeInserted_m5B03345EAC48958B104BA2033818370BA6E2243D(L_4, L_5, NULL);
		XmlDocument_t4DE82998E642C5C21A4A620A5278237C70D3E42B* L_6 = ___0_doc;
		XmlNodeChangedEventHandler_t907F6E9CB8DE238635028EC468AD41CAB0BA269B* L_7 = __this->___nodeChangeHandler;
		NullCheck(L_6);
		XmlDocument_add_NodeRemoved_m30754B20BE8A674BD5FDECF175E2419DD2AE3E1C(L_6, L_7, NULL);
		return;
	}
}
// Method Definition Index: 93921
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR void XmlElementListListener_OnListChanged_mD4D9B710020444018B7CA3A06C295978B38943DD (XmlElementListListener_t1513DCDD9C9C5B56690342DCC4445CB4BE6E7E79* __this, RuntimeObject* ___0_sender, XmlNodeChangedEventArgs_t965F2E02F8433933D197A0D1AF1EFEEDB524DF3A* ___1_args, const RuntimeMethod* method) 
{
	static bool s_Il2CppMethodInitialized;
	if (!s_Il2CppMethodInitialized)
	{
		il2cpp_codegen_initialize_runtime_metadata((uintptr_t*)&XmlElementList_tFE0D5337668FDE708796D7FDB669AE4A290CAB57_il2cpp_TypeInfo_var);
		s_Il2CppMethodInitialized = true;
	}
	XmlElementListListener_t1513DCDD9C9C5B56690342DCC4445CB4BE6E7E79* V_0 = NULL;
	bool V_1 = false;
	XmlElementList_tFE0D5337668FDE708796D7FDB669AE4A290CAB57* V_2 = NULL;
	{
		V_0 = __this;
		V_1 = (bool)0;
	}
	{
		auto __finallyBlock = il2cpp::utils::Finally([&]
		{

FINALLY_005c:
			{
				{
					bool L_0 = V_1;
					if (!L_0)
					{
						goto IL_0065;
					}
				}
				{
					XmlElementListListener_t1513DCDD9C9C5B56690342DCC4445CB4BE6E7E79* L_1 = V_0;
					Monitor_Exit_m05B2CF037E2214B3208198C282490A2A475653FA(L_1, NULL);
				}

IL_0065:
				{
					return;
				}
			}
		});
		try
		{
			{
				XmlElementListListener_t1513DCDD9C9C5B56690342DCC4445CB4BE6E7E79* L_2 = V_0;
				Monitor_Enter_m3CDB589DA1300B513D55FDCFB52B63E879794149(L_2, (&V_1), NULL);
				WeakReference_tD4B0518CE911FFD9FAAB3FCD492644A354312D8E* L_3 = __this->___elemList;
				if (!L_3)
				{
					goto IL_005a_1;
				}
			}
			{
				WeakReference_tD4B0518CE911FFD9FAAB3FCD492644A354312D8E* L_4 = __this->___elemList;
				NullCheck(L_4);
				RuntimeObject* L_5;
				L_5 = VirtualFuncInvoker0< RuntimeObject* >::Invoke(6, L_4);
				V_2 = ((XmlElementList_tFE0D5337668FDE708796D7FDB669AE4A290CAB57*)CastclassClass((RuntimeObject*)L_5, XmlElementList_tFE0D5337668FDE708796D7FDB669AE4A290CAB57_il2cpp_TypeInfo_var));
				XmlElementList_tFE0D5337668FDE708796D7FDB669AE4A290CAB57* L_6 = V_2;
				if (!L_6)
				{
					goto IL_0031_1;
				}
			}
			{
				XmlElementList_tFE0D5337668FDE708796D7FDB669AE4A290CAB57* L_7 = V_2;
				XmlNodeChangedEventArgs_t965F2E02F8433933D197A0D1AF1EFEEDB524DF3A* L_8 = ___1_args;
				NullCheck(L_7);
				XmlElementList_ConcurrencyCheck_mFC4A7DF2FC6B07D06430F714671AB3468E068386(L_7, L_8, NULL);
				goto IL_0066;
			}

IL_0031_1:
			{
				XmlDocument_t4DE82998E642C5C21A4A620A5278237C70D3E42B* L_9 = __this->___doc;
				XmlNodeChangedEventHandler_t907F6E9CB8DE238635028EC468AD41CAB0BA269B* L_10 = __this->___nodeChangeHandler;
				NullCheck(L_9);
				XmlDocument_remove_NodeInserted_mF46D0B84DB934188F551BCC766080CDA9A787616(L_9, L_10, NULL);
				XmlDocument_t4DE82998E642C5C21A4A620A5278237C70D3E42B* L_11 = __this->___doc;
				XmlNodeChangedEventHandler_t907F6E9CB8DE238635028EC468AD41CAB0BA269B* L_12 = __this->___nodeChangeHandler;
				NullCheck(L_11);
				XmlDocument_remove_NodeRemoved_m33B3778BE1EB94A010FDE8F860641821A7A079A3(L_11, L_12, NULL);
				__this->___elemList = (WeakReference_tD4B0518CE911FFD9FAAB3FCD492644A354312D8E*)NULL;
				Il2CppCodeGenWriteBarrier((void**)(&__this->___elemList), (void*)(WeakReference_tD4B0518CE911FFD9FAAB3FCD492644A354312D8E*)NULL);
			}

IL_005a_1:
			{
				goto IL_0066;
			}
		}
		catch(Il2CppExceptionWrapper& e)
		{
			__finallyBlock.StoreException(e.ex);
		}
	}

IL_0066:
	{
		return;
	}
}
// Method Definition Index: 93922
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR void XmlElementListListener_Unregister_m200394882802F40AE1337CE7EEC60ACAA606287C (XmlElementListListener_t1513DCDD9C9C5B56690342DCC4445CB4BE6E7E79* __this, const RuntimeMethod* method) 
{
	XmlElementListListener_t1513DCDD9C9C5B56690342DCC4445CB4BE6E7E79* V_0 = NULL;
	bool V_1 = false;
	{
		V_0 = __this;
		V_1 = (bool)0;
	}
	{
		auto __finallyBlock = il2cpp::utils::Finally([&]
		{

FINALLY_003f:
			{
				{
					bool L_0 = V_1;
					if (!L_0)
					{
						goto IL_0048;
					}
				}
				{
					XmlElementListListener_t1513DCDD9C9C5B56690342DCC4445CB4BE6E7E79* L_1 = V_0;
					Monitor_Exit_m05B2CF037E2214B3208198C282490A2A475653FA(L_1, NULL);
				}

IL_0048:
				{
					return;
				}
			}
		});
		try
		{
			{
				XmlElementListListener_t1513DCDD9C9C5B56690342DCC4445CB4BE6E7E79* L_2 = V_0;
				Monitor_Enter_m3CDB589DA1300B513D55FDCFB52B63E879794149(L_2, (&V_1), NULL);
				WeakReference_tD4B0518CE911FFD9FAAB3FCD492644A354312D8E* L_3 = __this->___elemList;
				if (!L_3)
				{
					goto IL_003d_1;
				}
			}
			{
				XmlDocument_t4DE82998E642C5C21A4A620A5278237C70D3E42B* L_4 = __this->___doc;
				XmlNodeChangedEventHandler_t907F6E9CB8DE238635028EC468AD41CAB0BA269B* L_5 = __this->___nodeChangeHandler;
				NullCheck(L_4);
				XmlDocument_remove_NodeInserted_mF46D0B84DB934188F551BCC766080CDA9A787616(L_4, L_5, NULL);
				XmlDocument_t4DE82998E642C5C21A4A620A5278237C70D3E42B* L_6 = __this->___doc;
				XmlNodeChangedEventHandler_t907F6E9CB8DE238635028EC468AD41CAB0BA269B* L_7 = __this->___nodeChangeHandler;
				NullCheck(L_6);
				XmlDocument_remove_NodeRemoved_m33B3778BE1EB94A010FDE8F860641821A7A079A3(L_6, L_7, NULL);
				__this->___elemList = (WeakReference_tD4B0518CE911FFD9FAAB3FCD492644A354312D8E*)NULL;
				Il2CppCodeGenWriteBarrier((void**)(&__this->___elemList), (void*)(WeakReference_tD4B0518CE911FFD9FAAB3FCD492644A354312D8E*)NULL);
			}

IL_003d_1:
			{
				goto IL_0049;
			}
		}
		catch(Il2CppExceptionWrapper& e)
		{
			__finallyBlock.StoreException(e.ex);
		}
	}

IL_0049:
	{
		return;
	}
}
#ifdef __clang__
#pragma clang diagnostic pop
#endif
#ifdef __clang__
#pragma clang diagnostic push
#pragma clang diagnostic ignored "-Winvalid-offsetof"
#pragma clang diagnostic ignored "-Wunused-variable"
#endif
#ifdef __clang__
#pragma clang diagnostic pop
#endif
#ifdef __clang__
#pragma clang diagnostic push
#pragma clang diagnostic ignored "-Winvalid-offsetof"
#pragma clang diagnostic ignored "-Wunused-variable"
#endif
// Method Definition Index: 93923
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR XmlNameTable_tBDBAACFF3DB40A8E6AF3BDC11F0FF166CF11ABB8* XmlImplementation_get_NameTable_m8951702F59619310BD4A9528D2FF471352518CDF (XmlImplementation_t4B3F467B76BD95C919C40424196C55B38EEC0F4D* __this, const RuntimeMethod* method) 
{
	{
		XmlNameTable_tBDBAACFF3DB40A8E6AF3BDC11F0FF166CF11ABB8* L_0 = __this->___nameTable;
		return L_0;
	}
}
#ifdef __clang__
#pragma clang diagnostic pop
#endif
#ifdef __clang__
#pragma clang diagnostic push
#pragma clang diagnostic ignored "-Winvalid-offsetof"
#pragma clang diagnostic ignored "-Wunused-variable"
#endif
#ifdef __clang__
#pragma clang diagnostic pop
#endif
#ifdef __clang__
#pragma clang diagnostic push
#pragma clang diagnostic ignored "-Winvalid-offsetof"
#pragma clang diagnostic ignored "-Wunused-variable"
#endif
// Method Definition Index: 93924
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR String_t* XmlName_get_LocalName_mD4DE8695A21CB393834F0DDFDD6D6B610E91A64D (XmlName_t0704430D24D202146901D342E34D878246E14F33* __this, const RuntimeMethod* method) 
{
	{
		String_t* L_0 = __this->___localName;
		return L_0;
	}
}
// Method Definition Index: 93925
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR int32_t XmlName_get_HashCode_m329A9FEAF3BDEAA5F1F40CB84F7E51E1BDE58B2D (XmlName_t0704430D24D202146901D342E34D878246E14F33* __this, const RuntimeMethod* method) 
{
	{
		int32_t L_0 = __this->___hashCode;
		return L_0;
	}
}
// Method Definition Index: 93926
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR String_t* XmlName_get_Name_m77B33DD36549E6E3E728EA1AD3325E339EC30435 (XmlName_t0704430D24D202146901D342E34D878246E14F33* __this, const RuntimeMethod* method) 
{
	static bool s_Il2CppMethodInitialized;
	if (!s_Il2CppMethodInitialized)
	{
		il2cpp_codegen_initialize_runtime_metadata((uintptr_t*)&_stringLiteral876C4B39B6E4D0187090400768899C71D99DE90D);
		s_Il2CppMethodInitialized = true;
	}
	String_t* V_0 = NULL;
	XmlNameTable_tBDBAACFF3DB40A8E6AF3BDC11F0FF166CF11ABB8* V_1 = NULL;
	bool V_2 = false;
	{
		String_t* L_0 = __this->___name;
		if (L_0)
		{
			goto IL_0099;
		}
	}
	{
		String_t* L_1 = __this->___prefix;
		NullCheck(L_1);
		int32_t L_2;
		L_2 = String_get_Length_m42625D67623FA5CC7A44D47425CE86FB946542D2_inline(L_1, NULL);
		if ((((int32_t)L_2) <= ((int32_t)0)))
		{
			goto IL_008d;
		}
	}
	{
		String_t* L_3 = __this->___localName;
		NullCheck(L_3);
		int32_t L_4;
		L_4 = String_get_Length_m42625D67623FA5CC7A44D47425CE86FB946542D2_inline(L_3, NULL);
		if ((((int32_t)L_4) <= ((int32_t)0)))
		{
			goto IL_007f;
		}
	}
	{
		String_t* L_5 = __this->___prefix;
		String_t* L_6 = __this->___localName;
		String_t* L_7;
		L_7 = String_Concat_m8855A6DE10F84DA7F4EC113CADDB59873A25573B(L_5, _stringLiteral876C4B39B6E4D0187090400768899C71D99DE90D, L_6, NULL);
		V_0 = L_7;
		XmlDocument_t4DE82998E642C5C21A4A620A5278237C70D3E42B* L_8 = __this->___ownerDoc;
		NullCheck(L_8);
		XmlNameTable_tBDBAACFF3DB40A8E6AF3BDC11F0FF166CF11ABB8* L_9;
		L_9 = XmlDocument_get_NameTable_m4B913865A24AEA917172F75CBDCE94C81CCB7E2C(L_8, NULL);
		V_1 = L_9;
		V_2 = (bool)0;
	}
	{
		auto __finallyBlock = il2cpp::utils::Finally([&]
		{

FINALLY_0075:
			{
				{
					bool L_10 = V_2;
					if (!L_10)
					{
						goto IL_007e;
					}
				}
				{
					XmlNameTable_tBDBAACFF3DB40A8E6AF3BDC11F0FF166CF11ABB8* L_11 = V_1;
					Monitor_Exit_m05B2CF037E2214B3208198C282490A2A475653FA(L_11, NULL);
				}

IL_007e:
				{
					return;
				}
			}
		});
		try
		{
			{
				XmlNameTable_tBDBAACFF3DB40A8E6AF3BDC11F0FF166CF11ABB8* L_12 = V_1;
				Monitor_Enter_m3CDB589DA1300B513D55FDCFB52B63E879794149(L_12, (&V_2), NULL);
				String_t* L_13 = __this->___name;
				if (L_13)
				{
					goto IL_0073_1;
				}
			}
			{
				XmlDocument_t4DE82998E642C5C21A4A620A5278237C70D3E42B* L_14 = __this->___ownerDoc;
				NullCheck(L_14);
				XmlNameTable_tBDBAACFF3DB40A8E6AF3BDC11F0FF166CF11ABB8* L_15;
				L_15 = XmlDocument_get_NameTable_m4B913865A24AEA917172F75CBDCE94C81CCB7E2C(L_14, NULL);
				String_t* L_16 = V_0;
				NullCheck(L_15);
				String_t* L_17;
				L_17 = VirtualFuncInvoker1< String_t*, String_t* >::Invoke(4, L_15, L_16);
				__this->___name = L_17;
				Il2CppCodeGenWriteBarrier((void**)(&__this->___name), (void*)L_17);
			}

IL_0073_1:
			{
				goto IL_0099;
			}
		}
		catch(Il2CppExceptionWrapper& e)
		{
			__finallyBlock.StoreException(e.ex);
		}
	}

IL_007f:
	{
		String_t* L_18 = __this->___prefix;
		__this->___name = L_18;
		Il2CppCodeGenWriteBarrier((void**)(&__this->___name), (void*)L_18);
		goto IL_0099;
	}

IL_008d:
	{
		String_t* L_19 = __this->___localName;
		__this->___name = L_19;
		Il2CppCodeGenWriteBarrier((void**)(&__this->___name), (void*)L_19);
	}

IL_0099:
	{
		String_t* L_20 = __this->___name;
		return L_20;
	}
}
// Method Definition Index: 93927
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR int32_t XmlName_GetHashCode_mC2C5513D62B0E10E556D1241BC33C4AFFAD1DDCD (String_t* ___0_name, const RuntimeMethod* method) 
{
	int32_t V_0 = 0;
	int32_t V_1 = 0;
	Il2CppChar V_2 = 0x0;
	{
		V_0 = 0;
		String_t* L_0 = ___0_name;
		if (!L_0)
		{
			goto IL_0041;
		}
	}
	{
		String_t* L_1 = ___0_name;
		NullCheck(L_1);
		int32_t L_2;
		L_2 = String_get_Length_m42625D67623FA5CC7A44D47425CE86FB946542D2_inline(L_1, NULL);
		V_1 = ((int32_t)il2cpp_codegen_subtract(L_2, 1));
		goto IL_0029;
	}

IL_0010:
	{
		String_t* L_3 = ___0_name;
		int32_t L_4 = V_1;
		NullCheck(L_3);
		Il2CppChar L_5;
		L_5 = String_get_Chars_mC49DF0CD2D3BE7BE97B3AD9C995BE3094F8E36D3(L_3, L_4, NULL);
		V_2 = L_5;
		Il2CppChar L_6 = V_2;
		if ((((int32_t)L_6) == ((int32_t)((int32_t)58))))
		{
			goto IL_002d;
		}
	}
	{
		int32_t L_7 = V_0;
		int32_t L_8 = V_0;
		Il2CppChar L_9 = V_2;
		V_0 = ((int32_t)il2cpp_codegen_add(L_7, ((int32_t)(((int32_t)(L_8<<7))^(int32_t)L_9))));
		int32_t L_10 = V_1;
		V_1 = ((int32_t)il2cpp_codegen_subtract(L_10, 1));
	}

IL_0029:
	{
		int32_t L_11 = V_1;
		if ((((int32_t)L_11) >= ((int32_t)0)))
		{
			goto IL_0010;
		}
	}

IL_002d:
	{
		int32_t L_12 = V_0;
		int32_t L_13 = V_0;
		V_0 = ((int32_t)il2cpp_codegen_subtract(L_12, ((int32_t)(L_13>>((int32_t)17)))));
		int32_t L_14 = V_0;
		int32_t L_15 = V_0;
		V_0 = ((int32_t)il2cpp_codegen_subtract(L_14, ((int32_t)(L_15>>((int32_t)11)))));
		int32_t L_16 = V_0;
		int32_t L_17 = V_0;
		V_0 = ((int32_t)il2cpp_codegen_subtract(L_16, ((int32_t)(L_17>>5))));
	}

IL_0041:
	{
		int32_t L_18 = V_0;
		return L_18;
	}
}
#ifdef __clang__
#pragma clang diagnostic pop
#endif
#ifdef __clang__
#pragma clang diagnostic push
#pragma clang diagnostic ignored "-Winvalid-offsetof"
#pragma clang diagnostic ignored "-Wunused-variable"
#endif
// Method Definition Index: 93928
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR int32_t XmlNamedNodeMap_get_Count_m383C4A91707F8FE7D905507CD35A043A5A7CFF29 (XmlNamedNodeMap_t13203D1B3861C62568AFFA1D644C04ABCBFC130C* __this, const RuntimeMethod* method) 
{
	{
		SmallXmlNodeList_tAF536F4BFCA26BF1B0E912818E921A3181D49EF0* L_0 = (SmallXmlNodeList_tAF536F4BFCA26BF1B0E912818E921A3181D49EF0*)(&__this->___nodes);
		int32_t L_1;
		L_1 = SmallXmlNodeList_get_Count_m79F61C89FC8ABD73BCB56938310461EFF1DF14BF(L_0, NULL);
		return L_1;
	}
}
#ifdef __clang__
#pragma clang diagnostic pop
#endif
#ifdef __clang__
#pragma clang diagnostic push
#pragma clang diagnostic ignored "-Winvalid-offsetof"
#pragma clang diagnostic ignored "-Wunused-variable"
#endif
IL2CPP_EXTERN_C void SmallXmlNodeList_tAF536F4BFCA26BF1B0E912818E921A3181D49EF0_marshal_pinvoke(const SmallXmlNodeList_tAF536F4BFCA26BF1B0E912818E921A3181D49EF0& unmarshaled, SmallXmlNodeList_tAF536F4BFCA26BF1B0E912818E921A3181D49EF0_marshaled_pinvoke& marshaled)
{
	if (unmarshaled.___field != NULL)
	{
		if (il2cpp_codegen_is_import_or_windows_runtime(unmarshaled.___field))
		{
			marshaled.___field = il2cpp_codegen_com_query_interface<Il2CppIUnknown>(static_cast<Il2CppComObject*>(unmarshaled.___field));
			(marshaled.___field)->AddRef();
		}
		else
		{
			marshaled.___field = il2cpp_codegen_com_get_or_create_ccw<Il2CppIUnknown>(unmarshaled.___field);
		}
	}
	else
	{
		marshaled.___field = NULL;
	}
}
IL2CPP_EXTERN_C void SmallXmlNodeList_tAF536F4BFCA26BF1B0E912818E921A3181D49EF0_marshal_pinvoke_back(const SmallXmlNodeList_tAF536F4BFCA26BF1B0E912818E921A3181D49EF0_marshaled_pinvoke& marshaled, SmallXmlNodeList_tAF536F4BFCA26BF1B0E912818E921A3181D49EF0& unmarshaled)
{
	static bool s_Il2CppMethodInitialized;
	if (!s_Il2CppMethodInitialized)
	{
		il2cpp_codegen_initialize_runtime_metadata((uintptr_t*)&Il2CppComObject_il2cpp_TypeInfo_var);
		s_Il2CppMethodInitialized = true;
	}
	if (marshaled.___field != NULL)
	{
		unmarshaled.___field = il2cpp_codegen_com_get_or_create_rcw_from_iunknown<RuntimeObject>(marshaled.___field, Il2CppComObject_il2cpp_TypeInfo_var);
		Il2CppCodeGenWriteBarrier((void**)(&unmarshaled.___field), (void*)il2cpp_codegen_com_get_or_create_rcw_from_iunknown<RuntimeObject>(marshaled.___field, Il2CppComObject_il2cpp_TypeInfo_var));

		if (il2cpp_codegen_is_import_or_windows_runtime(unmarshaled.___field))
		{
			il2cpp_codegen_com_cache_queried_interface(static_cast<Il2CppComObject*>(unmarshaled.___field), Il2CppIUnknown::IID, marshaled.___field);
		}
	}
	else
	{
		unmarshaled.___field = NULL;
		Il2CppCodeGenWriteBarrier((void**)(&unmarshaled.___field), (void*)NULL);
	}
}
IL2CPP_EXTERN_C void SmallXmlNodeList_tAF536F4BFCA26BF1B0E912818E921A3181D49EF0_marshal_pinvoke_cleanup(SmallXmlNodeList_tAF536F4BFCA26BF1B0E912818E921A3181D49EF0_marshaled_pinvoke& marshaled)
{
	if (marshaled.___field != NULL)
	{
		(marshaled.___field)->Release();
		marshaled.___field = NULL;
	}
}
IL2CPP_EXTERN_C void SmallXmlNodeList_tAF536F4BFCA26BF1B0E912818E921A3181D49EF0_marshal_com(const SmallXmlNodeList_tAF536F4BFCA26BF1B0E912818E921A3181D49EF0& unmarshaled, SmallXmlNodeList_tAF536F4BFCA26BF1B0E912818E921A3181D49EF0_marshaled_com& marshaled)
{
	if (unmarshaled.___field != NULL)
	{
		if (il2cpp_codegen_is_import_or_windows_runtime(unmarshaled.___field))
		{
			marshaled.___field = il2cpp_codegen_com_query_interface<Il2CppIUnknown>(static_cast<Il2CppComObject*>(unmarshaled.___field));
			(marshaled.___field)->AddRef();
		}
		else
		{
			marshaled.___field = il2cpp_codegen_com_get_or_create_ccw<Il2CppIUnknown>(unmarshaled.___field);
		}
	}
	else
	{
		marshaled.___field = NULL;
	}
}
IL2CPP_EXTERN_C void SmallXmlNodeList_tAF536F4BFCA26BF1B0E912818E921A3181D49EF0_marshal_com_back(const SmallXmlNodeList_tAF536F4BFCA26BF1B0E912818E921A3181D49EF0_marshaled_com& marshaled, SmallXmlNodeList_tAF536F4BFCA26BF1B0E912818E921A3181D49EF0& unmarshaled)
{
	static bool s_Il2CppMethodInitialized;
	if (!s_Il2CppMethodInitialized)
	{
		il2cpp_codegen_initialize_runtime_metadata((uintptr_t*)&Il2CppComObject_il2cpp_TypeInfo_var);
		s_Il2CppMethodInitialized = true;
	}
	if (marshaled.___field != NULL)
	{
		unmarshaled.___field = il2cpp_codegen_com_get_or_create_rcw_from_iunknown<RuntimeObject>(marshaled.___field, Il2CppComObject_il2cpp_TypeInfo_var);
		Il2CppCodeGenWriteBarrier((void**)(&unmarshaled.___field), (void*)il2cpp_codegen_com_get_or_create_rcw_from_iunknown<RuntimeObject>(marshaled.___field, Il2CppComObject_il2cpp_TypeInfo_var));

		if (il2cpp_codegen_is_import_or_windows_runtime(unmarshaled.___field))
		{
			il2cpp_codegen_com_cache_queried_interface(static_cast<Il2CppComObject*>(unmarshaled.___field), Il2CppIUnknown::IID, marshaled.___field);
		}
	}
	else
	{
		unmarshaled.___field = NULL;
		Il2CppCodeGenWriteBarrier((void**)(&unmarshaled.___field), (void*)NULL);
	}
}
IL2CPP_EXTERN_C void SmallXmlNodeList_tAF536F4BFCA26BF1B0E912818E921A3181D49EF0_marshal_com_cleanup(SmallXmlNodeList_tAF536F4BFCA26BF1B0E912818E921A3181D49EF0_marshaled_com& marshaled)
{
	if (marshaled.___field != NULL)
	{
		(marshaled.___field)->Release();
		marshaled.___field = NULL;
	}
}
// Method Definition Index: 93929
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR int32_t SmallXmlNodeList_get_Count_m79F61C89FC8ABD73BCB56938310461EFF1DF14BF (SmallXmlNodeList_tAF536F4BFCA26BF1B0E912818E921A3181D49EF0* __this, const RuntimeMethod* method) 
{
	static bool s_Il2CppMethodInitialized;
	if (!s_Il2CppMethodInitialized)
	{
		il2cpp_codegen_initialize_runtime_metadata((uintptr_t*)&ArrayList_t7A8E5AF0C4378015B5731ABE2BED8F2782FEEF8A_il2cpp_TypeInfo_var);
		s_Il2CppMethodInitialized = true;
	}
	ArrayList_t7A8E5AF0C4378015B5731ABE2BED8F2782FEEF8A* V_0 = NULL;
	{
		RuntimeObject* L_0 = __this->___field;
		if (L_0)
		{
			goto IL_000a;
		}
	}
	{
		return 0;
	}

IL_000a:
	{
		RuntimeObject* L_1 = __this->___field;
		V_0 = ((ArrayList_t7A8E5AF0C4378015B5731ABE2BED8F2782FEEF8A*)IsInstClass((RuntimeObject*)L_1, ArrayList_t7A8E5AF0C4378015B5731ABE2BED8F2782FEEF8A_il2cpp_TypeInfo_var));
		ArrayList_t7A8E5AF0C4378015B5731ABE2BED8F2782FEEF8A* L_2 = V_0;
		if (!L_2)
		{
			goto IL_0020;
		}
	}
	{
		ArrayList_t7A8E5AF0C4378015B5731ABE2BED8F2782FEEF8A* L_3 = V_0;
		NullCheck(L_3);
		int32_t L_4;
		L_4 = VirtualFuncInvoker0< int32_t >::Invoke(21, L_3);
		return L_4;
	}

IL_0020:
	{
		return 1;
	}
}
IL2CPP_EXTERN_C  int32_t SmallXmlNodeList_get_Count_m79F61C89FC8ABD73BCB56938310461EFF1DF14BF_AdjustorThunk (RuntimeObject* __this, const RuntimeMethod* method)
{
	SmallXmlNodeList_tAF536F4BFCA26BF1B0E912818E921A3181D49EF0* _thisAdjusted;
	int32_t _offset = 1;
	_thisAdjusted = reinterpret_cast<SmallXmlNodeList_tAF536F4BFCA26BF1B0E912818E921A3181D49EF0*>(__this + _offset);
	int32_t _returnValue;
	_returnValue = SmallXmlNodeList_get_Count_m79F61C89FC8ABD73BCB56938310461EFF1DF14BF(_thisAdjusted, method);
	return _returnValue;
}
// Method Definition Index: 93930
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR RuntimeObject* SmallXmlNodeList_get_Item_mFE828A47A297981D034EFC9244E53B3D34FC9576 (SmallXmlNodeList_tAF536F4BFCA26BF1B0E912818E921A3181D49EF0* __this, int32_t ___0_index, const RuntimeMethod* method) 
{
	static bool s_Il2CppMethodInitialized;
	if (!s_Il2CppMethodInitialized)
	{
		il2cpp_codegen_initialize_runtime_metadata((uintptr_t*)&ArrayList_t7A8E5AF0C4378015B5731ABE2BED8F2782FEEF8A_il2cpp_TypeInfo_var);
		s_Il2CppMethodInitialized = true;
	}
	ArrayList_t7A8E5AF0C4378015B5731ABE2BED8F2782FEEF8A* V_0 = NULL;
	{
		RuntimeObject* L_0 = __this->___field;
		if (L_0)
		{
			goto IL_0013;
		}
	}
	{
		ArgumentOutOfRangeException_tEA2822DAF62B10EEED00E0E3A341D4BAF78CF85F* L_1 = (ArgumentOutOfRangeException_tEA2822DAF62B10EEED00E0E3A341D4BAF78CF85F*)il2cpp_codegen_object_new(((RuntimeClass*)il2cpp_codegen_initialize_runtime_metadata_inline((uintptr_t*)&ArgumentOutOfRangeException_tEA2822DAF62B10EEED00E0E3A341D4BAF78CF85F_il2cpp_TypeInfo_var)));
		ArgumentOutOfRangeException__ctor_mBC1D5DEEA1BA41DE77228CB27D6BAFEB6DCCBF4A(L_1, ((String_t*)il2cpp_codegen_initialize_runtime_metadata_inline((uintptr_t*)&_stringLiteral2B6D6F48C27C60C3B55391AB377D9DC8F5639AA1)), NULL);
		IL2CPP_RAISE_MANAGED_EXCEPTION(L_1, ((RuntimeMethod*)il2cpp_codegen_initialize_runtime_metadata_inline((uintptr_t*)&SmallXmlNodeList_get_Item_mFE828A47A297981D034EFC9244E53B3D34FC9576_RuntimeMethod_var)));
	}

IL_0013:
	{
		RuntimeObject* L_2 = __this->___field;
		V_0 = ((ArrayList_t7A8E5AF0C4378015B5731ABE2BED8F2782FEEF8A*)IsInstClass((RuntimeObject*)L_2, ArrayList_t7A8E5AF0C4378015B5731ABE2BED8F2782FEEF8A_il2cpp_TypeInfo_var));
		ArrayList_t7A8E5AF0C4378015B5731ABE2BED8F2782FEEF8A* L_3 = V_0;
		if (!L_3)
		{
			goto IL_002a;
		}
	}
	{
		ArrayList_t7A8E5AF0C4378015B5731ABE2BED8F2782FEEF8A* L_4 = V_0;
		int32_t L_5 = ___0_index;
		NullCheck(L_4);
		RuntimeObject* L_6;
		L_6 = VirtualFuncInvoker1< RuntimeObject*, int32_t >::Invoke(25, L_4, L_5);
		return L_6;
	}

IL_002a:
	{
		int32_t L_7 = ___0_index;
		if (!L_7)
		{
			goto IL_0038;
		}
	}
	{
		ArgumentOutOfRangeException_tEA2822DAF62B10EEED00E0E3A341D4BAF78CF85F* L_8 = (ArgumentOutOfRangeException_tEA2822DAF62B10EEED00E0E3A341D4BAF78CF85F*)il2cpp_codegen_object_new(((RuntimeClass*)il2cpp_codegen_initialize_runtime_metadata_inline((uintptr_t*)&ArgumentOutOfRangeException_tEA2822DAF62B10EEED00E0E3A341D4BAF78CF85F_il2cpp_TypeInfo_var)));
		ArgumentOutOfRangeException__ctor_mBC1D5DEEA1BA41DE77228CB27D6BAFEB6DCCBF4A(L_8, ((String_t*)il2cpp_codegen_initialize_runtime_metadata_inline((uintptr_t*)&_stringLiteral2B6D6F48C27C60C3B55391AB377D9DC8F5639AA1)), NULL);
		IL2CPP_RAISE_MANAGED_EXCEPTION(L_8, ((RuntimeMethod*)il2cpp_codegen_initialize_runtime_metadata_inline((uintptr_t*)&SmallXmlNodeList_get_Item_mFE828A47A297981D034EFC9244E53B3D34FC9576_RuntimeMethod_var)));
	}

IL_0038:
	{
		RuntimeObject* L_9 = __this->___field;
		return L_9;
	}
}
IL2CPP_EXTERN_C  RuntimeObject* SmallXmlNodeList_get_Item_mFE828A47A297981D034EFC9244E53B3D34FC9576_AdjustorThunk (RuntimeObject* __this, int32_t ___0_index, const RuntimeMethod* method)
{
	SmallXmlNodeList_tAF536F4BFCA26BF1B0E912818E921A3181D49EF0* _thisAdjusted;
	int32_t _offset = 1;
	_thisAdjusted = reinterpret_cast<SmallXmlNodeList_tAF536F4BFCA26BF1B0E912818E921A3181D49EF0*>(__this + _offset);
	RuntimeObject* _returnValue;
	_returnValue = SmallXmlNodeList_get_Item_mFE828A47A297981D034EFC9244E53B3D34FC9576(_thisAdjusted, ___0_index, method);
	return _returnValue;
}
#ifdef __clang__
#pragma clang diagnostic pop
#endif
#ifdef __clang__
#pragma clang diagnostic push
#pragma clang diagnostic ignored "-Winvalid-offsetof"
#pragma clang diagnostic ignored "-Wunused-variable"
#endif
// Method Definition Index: 93932
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR String_t* XmlNode_get_Value_m21CB160306F9CE778A143C9D753E99F8EF801592 (XmlNode_t3180B9B3D5C36CD58F5327D9F13458E3B3F030AF* __this, const RuntimeMethod* method) 
{
	{
		return (String_t*)NULL;
	}
}
// Method Definition Index: 93934
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR XmlNode_t3180B9B3D5C36CD58F5327D9F13458E3B3F030AF* XmlNode_get_ParentNode_mF576AC1CF834069B79BF32E497BB1A05E3E4AF63 (XmlNode_t3180B9B3D5C36CD58F5327D9F13458E3B3F030AF* __this, const RuntimeMethod* method) 
{
	static bool s_Il2CppMethodInitialized;
	if (!s_Il2CppMethodInitialized)
	{
		il2cpp_codegen_initialize_runtime_metadata((uintptr_t*)&XmlLinkedNode_t640BF5D3FDAF0606665C3BAE565988A5014F1B9C_il2cpp_TypeInfo_var);
		s_Il2CppMethodInitialized = true;
	}
	XmlLinkedNode_t640BF5D3FDAF0606665C3BAE565988A5014F1B9C* V_0 = NULL;
	XmlLinkedNode_t640BF5D3FDAF0606665C3BAE565988A5014F1B9C* V_1 = NULL;
	{
		XmlNode_t3180B9B3D5C36CD58F5327D9F13458E3B3F030AF* L_0 = __this->___parentNode;
		NullCheck(L_0);
		int32_t L_1;
		L_1 = VirtualFuncInvoker0< int32_t >::Invoke(6, L_0);
		if ((((int32_t)L_1) == ((int32_t)((int32_t)9))))
		{
			goto IL_0016;
		}
	}
	{
		XmlNode_t3180B9B3D5C36CD58F5327D9F13458E3B3F030AF* L_2 = __this->___parentNode;
		return L_2;
	}

IL_0016:
	{
		XmlNode_t3180B9B3D5C36CD58F5327D9F13458E3B3F030AF* L_3 = __this->___parentNode;
		NullCheck(L_3);
		XmlNode_t3180B9B3D5C36CD58F5327D9F13458E3B3F030AF* L_4;
		L_4 = VirtualFuncInvoker0< XmlNode_t3180B9B3D5C36CD58F5327D9F13458E3B3F030AF* >::Invoke(13, L_3);
		V_0 = ((XmlLinkedNode_t640BF5D3FDAF0606665C3BAE565988A5014F1B9C*)IsInstClass((RuntimeObject*)L_4, XmlLinkedNode_t640BF5D3FDAF0606665C3BAE565988A5014F1B9C_il2cpp_TypeInfo_var));
		XmlLinkedNode_t640BF5D3FDAF0606665C3BAE565988A5014F1B9C* L_5 = V_0;
		if (!L_5)
		{
			goto IL_0045;
		}
	}
	{
		XmlLinkedNode_t640BF5D3FDAF0606665C3BAE565988A5014F1B9C* L_6 = V_0;
		V_1 = L_6;
	}

IL_002c:
	{
		XmlLinkedNode_t640BF5D3FDAF0606665C3BAE565988A5014F1B9C* L_7 = V_1;
		if ((!(((RuntimeObject*)(XmlLinkedNode_t640BF5D3FDAF0606665C3BAE565988A5014F1B9C*)L_7) == ((RuntimeObject*)(XmlNode_t3180B9B3D5C36CD58F5327D9F13458E3B3F030AF*)__this))))
		{
			goto IL_0037;
		}
	}
	{
		XmlNode_t3180B9B3D5C36CD58F5327D9F13458E3B3F030AF* L_8 = __this->___parentNode;
		return L_8;
	}

IL_0037:
	{
		XmlLinkedNode_t640BF5D3FDAF0606665C3BAE565988A5014F1B9C* L_9 = V_1;
		NullCheck(L_9);
		XmlLinkedNode_t640BF5D3FDAF0606665C3BAE565988A5014F1B9C* L_10 = L_9->___next;
		V_1 = L_10;
		XmlLinkedNode_t640BF5D3FDAF0606665C3BAE565988A5014F1B9C* L_11 = V_1;
		if (!L_11)
		{
			goto IL_0045;
		}
	}
	{
		XmlLinkedNode_t640BF5D3FDAF0606665C3BAE565988A5014F1B9C* L_12 = V_1;
		XmlLinkedNode_t640BF5D3FDAF0606665C3BAE565988A5014F1B9C* L_13 = V_0;
		if ((!(((RuntimeObject*)(XmlLinkedNode_t640BF5D3FDAF0606665C3BAE565988A5014F1B9C*)L_12) == ((RuntimeObject*)(XmlLinkedNode_t640BF5D3FDAF0606665C3BAE565988A5014F1B9C*)L_13))))
		{
			goto IL_002c;
		}
	}

IL_0045:
	{
		return (XmlNode_t3180B9B3D5C36CD58F5327D9F13458E3B3F030AF*)NULL;
	}
}
// Method Definition Index: 93935
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR XmlNodeList_tFC0CEED3A006BEDF6A1A420F7CB55AC33E9B7F85* XmlNode_get_ChildNodes_m6437D40A51810BEF219E307A3821C80DB48B1FA7 (XmlNode_t3180B9B3D5C36CD58F5327D9F13458E3B3F030AF* __this, const RuntimeMethod* method) 
{
	static bool s_Il2CppMethodInitialized;
	if (!s_Il2CppMethodInitialized)
	{
		il2cpp_codegen_initialize_runtime_metadata((uintptr_t*)&XmlChildNodes_tBD3126123F4810D8B272B8201C07FE70CDC9C5E2_il2cpp_TypeInfo_var);
		s_Il2CppMethodInitialized = true;
	}
	{
		XmlChildNodes_tBD3126123F4810D8B272B8201C07FE70CDC9C5E2* L_0 = (XmlChildNodes_tBD3126123F4810D8B272B8201C07FE70CDC9C5E2*)il2cpp_codegen_object_new(XmlChildNodes_tBD3126123F4810D8B272B8201C07FE70CDC9C5E2_il2cpp_TypeInfo_var);
		XmlChildNodes__ctor_mE297CC7E680D376A26339D6067C7E434271234B5(L_0, __this, NULL);
		return L_0;
	}
}
// Method Definition Index: 93936
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR XmlNode_t3180B9B3D5C36CD58F5327D9F13458E3B3F030AF* XmlNode_get_PreviousSibling_mF0D096FB5E4D09F658F36BF1B5EFFC5B60377045 (XmlNode_t3180B9B3D5C36CD58F5327D9F13458E3B3F030AF* __this, const RuntimeMethod* method) 
{
	{
		return (XmlNode_t3180B9B3D5C36CD58F5327D9F13458E3B3F030AF*)NULL;
	}
}
// Method Definition Index: 93937
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR XmlNode_t3180B9B3D5C36CD58F5327D9F13458E3B3F030AF* XmlNode_get_NextSibling_mB3B3EFD4597AF366F3010C0CC80DC5C5B6885492 (XmlNode_t3180B9B3D5C36CD58F5327D9F13458E3B3F030AF* __this, const RuntimeMethod* method) 
{
	{
		return (XmlNode_t3180B9B3D5C36CD58F5327D9F13458E3B3F030AF*)NULL;
	}
}
// Method Definition Index: 93938
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR XmlAttributeCollection_tB0BA25B3C7E8D4BCF487C7107A9019632A7D85C0* XmlNode_get_Attributes_m04E6956D2BBF4491EF6310F83E382B1416378EB7 (XmlNode_t3180B9B3D5C36CD58F5327D9F13458E3B3F030AF* __this, const RuntimeMethod* method) 
{
	{
		return (XmlAttributeCollection_tB0BA25B3C7E8D4BCF487C7107A9019632A7D85C0*)NULL;
	}
}
// Method Definition Index: 93939
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR XmlDocument_t4DE82998E642C5C21A4A620A5278237C70D3E42B* XmlNode_get_OwnerDocument_m202868042E0CA116D7C337701E1140E16E4624EE (XmlNode_t3180B9B3D5C36CD58F5327D9F13458E3B3F030AF* __this, const RuntimeMethod* method) 
{
	static bool s_Il2CppMethodInitialized;
	if (!s_Il2CppMethodInitialized)
	{
		il2cpp_codegen_initialize_runtime_metadata((uintptr_t*)&XmlDocument_t4DE82998E642C5C21A4A620A5278237C70D3E42B_il2cpp_TypeInfo_var);
		s_Il2CppMethodInitialized = true;
	}
	{
		XmlNode_t3180B9B3D5C36CD58F5327D9F13458E3B3F030AF* L_0 = __this->___parentNode;
		NullCheck(L_0);
		int32_t L_1;
		L_1 = VirtualFuncInvoker0< int32_t >::Invoke(6, L_0);
		if ((!(((uint32_t)L_1) == ((uint32_t)((int32_t)9)))))
		{
			goto IL_001b;
		}
	}
	{
		XmlNode_t3180B9B3D5C36CD58F5327D9F13458E3B3F030AF* L_2 = __this->___parentNode;
		return ((XmlDocument_t4DE82998E642C5C21A4A620A5278237C70D3E42B*)CastclassClass((RuntimeObject*)L_2, XmlDocument_t4DE82998E642C5C21A4A620A5278237C70D3E42B_il2cpp_TypeInfo_var));
	}

IL_001b:
	{
		XmlNode_t3180B9B3D5C36CD58F5327D9F13458E3B3F030AF* L_3 = __this->___parentNode;
		NullCheck(L_3);
		XmlDocument_t4DE82998E642C5C21A4A620A5278237C70D3E42B* L_4;
		L_4 = VirtualFuncInvoker0< XmlDocument_t4DE82998E642C5C21A4A620A5278237C70D3E42B* >::Invoke(12, L_3);
		return L_4;
	}
}
// Method Definition Index: 93940
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR XmlNode_t3180B9B3D5C36CD58F5327D9F13458E3B3F030AF* XmlNode_get_FirstChild_m5F667C696492597141F3F92F048DB73C5546F1CD (XmlNode_t3180B9B3D5C36CD58F5327D9F13458E3B3F030AF* __this, const RuntimeMethod* method) 
{
	XmlLinkedNode_t640BF5D3FDAF0606665C3BAE565988A5014F1B9C* V_0 = NULL;
	{
		XmlLinkedNode_t640BF5D3FDAF0606665C3BAE565988A5014F1B9C* L_0;
		L_0 = VirtualFuncInvoker0< XmlLinkedNode_t640BF5D3FDAF0606665C3BAE565988A5014F1B9C* >::Invoke(15, __this);
		V_0 = L_0;
		XmlLinkedNode_t640BF5D3FDAF0606665C3BAE565988A5014F1B9C* L_1 = V_0;
		if (!L_1)
		{
			goto IL_0011;
		}
	}
	{
		XmlLinkedNode_t640BF5D3FDAF0606665C3BAE565988A5014F1B9C* L_2 = V_0;
		NullCheck(L_2);
		XmlLinkedNode_t640BF5D3FDAF0606665C3BAE565988A5014F1B9C* L_3 = L_2->___next;
		return L_3;
	}

IL_0011:
	{
		return (XmlNode_t3180B9B3D5C36CD58F5327D9F13458E3B3F030AF*)NULL;
	}
}
// Method Definition Index: 93941
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR XmlNode_t3180B9B3D5C36CD58F5327D9F13458E3B3F030AF* XmlNode_get_LastChild_m65D979AFE8805E2D45EF2F2C8DD18727F1286E7E (XmlNode_t3180B9B3D5C36CD58F5327D9F13458E3B3F030AF* __this, const RuntimeMethod* method) 
{
	{
		XmlLinkedNode_t640BF5D3FDAF0606665C3BAE565988A5014F1B9C* L_0;
		L_0 = VirtualFuncInvoker0< XmlLinkedNode_t640BF5D3FDAF0606665C3BAE565988A5014F1B9C* >::Invoke(15, __this);
		return L_0;
	}
}
// Method Definition Index: 93942
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR XmlLinkedNode_t640BF5D3FDAF0606665C3BAE565988A5014F1B9C* XmlNode_get_LastNode_mBE8307841D326A20F838DB33C8487C53FD0394A7 (XmlNode_t3180B9B3D5C36CD58F5327D9F13458E3B3F030AF* __this, const RuntimeMethod* method) 
{
	{
		return (XmlLinkedNode_t640BF5D3FDAF0606665C3BAE565988A5014F1B9C*)NULL;
	}
}
// Method Definition Index: 93943
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR String_t* XmlNode_get_NamespaceURI_mE894547F6A17FE4B59E2733E1C0BF2046B50A494 (XmlNode_t3180B9B3D5C36CD58F5327D9F13458E3B3F030AF* __this, const RuntimeMethod* method) 
{
	{
		String_t* L_0 = ((String_t_StaticFields*)il2cpp_codegen_static_fields_for(il2cpp_defaults.string_class))->___Empty;
		return L_0;
	}
}
// Method Definition Index: 93945
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR void XmlNode_AppendChildText_m8C1493DF2F031942BD58F96DE8EFC365194561A1 (XmlNode_t3180B9B3D5C36CD58F5327D9F13458E3B3F030AF* __this, StringBuilder_t* ___0_builder, const RuntimeMethod* method) 
{
	XmlNode_t3180B9B3D5C36CD58F5327D9F13458E3B3F030AF* V_0 = NULL;
	{
		XmlNode_t3180B9B3D5C36CD58F5327D9F13458E3B3F030AF* L_0;
		L_0 = VirtualFuncInvoker0< XmlNode_t3180B9B3D5C36CD58F5327D9F13458E3B3F030AF* >::Invoke(13, __this);
		V_0 = L_0;
		goto IL_0054;
	}

IL_0009:
	{
		XmlNode_t3180B9B3D5C36CD58F5327D9F13458E3B3F030AF* L_1 = V_0;
		NullCheck(L_1);
		XmlNode_t3180B9B3D5C36CD58F5327D9F13458E3B3F030AF* L_2;
		L_2 = VirtualFuncInvoker0< XmlNode_t3180B9B3D5C36CD58F5327D9F13458E3B3F030AF* >::Invoke(13, L_1);
		if (L_2)
		{
			goto IL_0046;
		}
	}
	{
		XmlNode_t3180B9B3D5C36CD58F5327D9F13458E3B3F030AF* L_3 = V_0;
		NullCheck(L_3);
		int32_t L_4;
		L_4 = VirtualFuncInvoker0< int32_t >::Invoke(6, L_3);
		if ((((int32_t)L_4) == ((int32_t)3)))
		{
			goto IL_0037;
		}
	}
	{
		XmlNode_t3180B9B3D5C36CD58F5327D9F13458E3B3F030AF* L_5 = V_0;
		NullCheck(L_5);
		int32_t L_6;
		L_6 = VirtualFuncInvoker0< int32_t >::Invoke(6, L_5);
		if ((((int32_t)L_6) == ((int32_t)4)))
		{
			goto IL_0037;
		}
	}
	{
		XmlNode_t3180B9B3D5C36CD58F5327D9F13458E3B3F030AF* L_7 = V_0;
		NullCheck(L_7);
		int32_t L_8;
		L_8 = VirtualFuncInvoker0< int32_t >::Invoke(6, L_7);
		if ((((int32_t)L_8) == ((int32_t)((int32_t)13))))
		{
			goto IL_0037;
		}
	}
	{
		XmlNode_t3180B9B3D5C36CD58F5327D9F13458E3B3F030AF* L_9 = V_0;
		NullCheck(L_9);
		int32_t L_10;
		L_10 = VirtualFuncInvoker0< int32_t >::Invoke(6, L_9);
		if ((!(((uint32_t)L_10) == ((uint32_t)((int32_t)14)))))
		{
			goto IL_004d;
		}
	}

IL_0037:
	{
		StringBuilder_t* L_11 = ___0_builder;
		XmlNode_t3180B9B3D5C36CD58F5327D9F13458E3B3F030AF* L_12 = V_0;
		NullCheck(L_12);
		String_t* L_13;
		L_13 = VirtualFuncInvoker0< String_t* >::Invoke(18, L_12);
		NullCheck(L_11);
		StringBuilder_t* L_14;
		L_14 = StringBuilder_Append_m08904D74E0C78E5F36DCD9C9303BDD07886D9F7D(L_11, L_13, NULL);
		goto IL_004d;
	}

IL_0046:
	{
		XmlNode_t3180B9B3D5C36CD58F5327D9F13458E3B3F030AF* L_15 = V_0;
		StringBuilder_t* L_16 = ___0_builder;
		NullCheck(L_15);
		XmlNode_AppendChildText_m8C1493DF2F031942BD58F96DE8EFC365194561A1(L_15, L_16, NULL);
	}

IL_004d:
	{
		XmlNode_t3180B9B3D5C36CD58F5327D9F13458E3B3F030AF* L_17 = V_0;
		NullCheck(L_17);
		XmlNode_t3180B9B3D5C36CD58F5327D9F13458E3B3F030AF* L_18;
		L_18 = VirtualFuncInvoker0< XmlNode_t3180B9B3D5C36CD58F5327D9F13458E3B3F030AF* >::Invoke(10, L_17);
		V_0 = L_18;
	}

IL_0054:
	{
		XmlNode_t3180B9B3D5C36CD58F5327D9F13458E3B3F030AF* L_19 = V_0;
		if (L_19)
		{
			goto IL_0009;
		}
	}
	{
		return;
	}
}
// Method Definition Index: 93946
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR String_t* XmlNode_get_InnerText_mC66D119E27E20F4C1E473CB76C8209CDA7262C8D (XmlNode_t3180B9B3D5C36CD58F5327D9F13458E3B3F030AF* __this, const RuntimeMethod* method) 
{
	static bool s_Il2CppMethodInitialized;
	if (!s_Il2CppMethodInitialized)
	{
		il2cpp_codegen_initialize_runtime_metadata((uintptr_t*)&StringBuilder_t_il2cpp_TypeInfo_var);
		s_Il2CppMethodInitialized = true;
	}
	XmlNode_t3180B9B3D5C36CD58F5327D9F13458E3B3F030AF* V_0 = NULL;
	StringBuilder_t* V_1 = NULL;
	int32_t V_2 = 0;
	{
		XmlNode_t3180B9B3D5C36CD58F5327D9F13458E3B3F030AF* L_0;
		L_0 = VirtualFuncInvoker0< XmlNode_t3180B9B3D5C36CD58F5327D9F13458E3B3F030AF* >::Invoke(13, __this);
		V_0 = L_0;
		XmlNode_t3180B9B3D5C36CD58F5327D9F13458E3B3F030AF* L_1 = V_0;
		if (L_1)
		{
			goto IL_0010;
		}
	}
	{
		String_t* L_2 = ((String_t_StaticFields*)il2cpp_codegen_static_fields_for(il2cpp_defaults.string_class))->___Empty;
		return L_2;
	}

IL_0010:
	{
		XmlNode_t3180B9B3D5C36CD58F5327D9F13458E3B3F030AF* L_3 = V_0;
		NullCheck(L_3);
		XmlNode_t3180B9B3D5C36CD58F5327D9F13458E3B3F030AF* L_4;
		L_4 = VirtualFuncInvoker0< XmlNode_t3180B9B3D5C36CD58F5327D9F13458E3B3F030AF* >::Invoke(10, L_3);
		if (L_4)
		{
			goto IL_0033;
		}
	}
	{
		XmlNode_t3180B9B3D5C36CD58F5327D9F13458E3B3F030AF* L_5 = V_0;
		NullCheck(L_5);
		int32_t L_6;
		L_6 = VirtualFuncInvoker0< int32_t >::Invoke(6, L_5);
		V_2 = L_6;
		int32_t L_7 = V_2;
		if ((!(((uint32_t)((int32_t)il2cpp_codegen_subtract((int32_t)L_7, 3))) > ((uint32_t)1))))
		{
			goto IL_002c;
		}
	}
	{
		int32_t L_8 = V_2;
		if ((!(((uint32_t)((int32_t)il2cpp_codegen_subtract((int32_t)L_8, ((int32_t)13)))) <= ((uint32_t)1))))
		{
			goto IL_0033;
		}
	}

IL_002c:
	{
		XmlNode_t3180B9B3D5C36CD58F5327D9F13458E3B3F030AF* L_9 = V_0;
		NullCheck(L_9);
		String_t* L_10;
		L_10 = VirtualFuncInvoker0< String_t* >::Invoke(5, L_9);
		return L_10;
	}

IL_0033:
	{
		StringBuilder_t* L_11 = (StringBuilder_t*)il2cpp_codegen_object_new(StringBuilder_t_il2cpp_TypeInfo_var);
		StringBuilder__ctor_m1D99713357DE05DAFA296633639DB55F8C30587D(L_11, NULL);
		V_1 = L_11;
		StringBuilder_t* L_12 = V_1;
		XmlNode_AppendChildText_m8C1493DF2F031942BD58F96DE8EFC365194561A1(__this, L_12, NULL);
		StringBuilder_t* L_13 = V_1;
		NullCheck(L_13);
		String_t* L_14;
		L_14 = VirtualFuncInvoker0< String_t* >::Invoke(3, L_13);
		return L_14;
	}
}
// Method Definition Index: 93947
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR XmlDocument_t4DE82998E642C5C21A4A620A5278237C70D3E42B* XmlNode_get_Document_mF9753FAD0D7BFDBDAE7FEA6B196D7CA2982A373C (XmlNode_t3180B9B3D5C36CD58F5327D9F13458E3B3F030AF* __this, const RuntimeMethod* method) 
{
	static bool s_Il2CppMethodInitialized;
	if (!s_Il2CppMethodInitialized)
	{
		il2cpp_codegen_initialize_runtime_metadata((uintptr_t*)&XmlDocument_t4DE82998E642C5C21A4A620A5278237C70D3E42B_il2cpp_TypeInfo_var);
		s_Il2CppMethodInitialized = true;
	}
	{
		int32_t L_0;
		L_0 = VirtualFuncInvoker0< int32_t >::Invoke(6, __this);
		if ((!(((uint32_t)L_0) == ((uint32_t)((int32_t)9)))))
		{
			goto IL_0011;
		}
	}
	{
		return ((XmlDocument_t4DE82998E642C5C21A4A620A5278237C70D3E42B*)CastclassClass((RuntimeObject*)__this, XmlDocument_t4DE82998E642C5C21A4A620A5278237C70D3E42B_il2cpp_TypeInfo_var));
	}

IL_0011:
	{
		XmlDocument_t4DE82998E642C5C21A4A620A5278237C70D3E42B* L_1;
		L_1 = VirtualFuncInvoker0< XmlDocument_t4DE82998E642C5C21A4A620A5278237C70D3E42B* >::Invoke(12, __this);
		return L_1;
	}
}
// Method Definition Index: 93948
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR RuntimeObject* XmlNode_get_debuggerDisplayProxy_m3E2D46C8120C46626198C092C4663DA5FE71F373 (XmlNode_t3180B9B3D5C36CD58F5327D9F13458E3B3F030AF* __this, const RuntimeMethod* method) 
{
	static bool s_Il2CppMethodInitialized;
	if (!s_Il2CppMethodInitialized)
	{
		il2cpp_codegen_initialize_runtime_metadata((uintptr_t*)&DebuggerDisplayXmlNodeProxy_t75382D048931DB9A7D8EFB35A56EEC1D8241FB07_il2cpp_TypeInfo_var);
		s_Il2CppMethodInitialized = true;
	}
	{
		DebuggerDisplayXmlNodeProxy_t75382D048931DB9A7D8EFB35A56EEC1D8241FB07 L_0;
		memset((&L_0), 0, sizeof(L_0));
		DebuggerDisplayXmlNodeProxy__ctor_mB9B6D906F2EBADBBDBA79FE184D6860363C32356_inline((&L_0), __this, NULL);
		DebuggerDisplayXmlNodeProxy_t75382D048931DB9A7D8EFB35A56EEC1D8241FB07 L_1 = L_0;
		RuntimeObject* L_2 = Box(DebuggerDisplayXmlNodeProxy_t75382D048931DB9A7D8EFB35A56EEC1D8241FB07_il2cpp_TypeInfo_var, &L_1);
		return L_2;
	}
}
#ifdef __clang__
#pragma clang diagnostic pop
#endif
#ifdef __clang__
#pragma clang diagnostic push
#pragma clang diagnostic ignored "-Winvalid-offsetof"
#pragma clang diagnostic ignored "-Wunused-variable"
#endif
IL2CPP_EXTERN_C void DebuggerDisplayXmlNodeProxy_t75382D048931DB9A7D8EFB35A56EEC1D8241FB07_marshal_pinvoke(const DebuggerDisplayXmlNodeProxy_t75382D048931DB9A7D8EFB35A56EEC1D8241FB07& unmarshaled, DebuggerDisplayXmlNodeProxy_t75382D048931DB9A7D8EFB35A56EEC1D8241FB07_marshaled_pinvoke& marshaled)
{
	static bool s_Il2CppMethodInitialized;
	if (!s_Il2CppMethodInitialized)
	{
		il2cpp_codegen_initialize_runtime_metadata((uintptr_t*)&DebuggerDisplayXmlNodeProxy_t75382D048931DB9A7D8EFB35A56EEC1D8241FB07_0_0_0_var);
		il2cpp_codegen_initialize_runtime_metadata((uintptr_t*)&DebuggerDisplayXmlNodeProxy_t75382D048931DB9A7D8EFB35A56EEC1D8241FB07____node_FieldInfo_var);
		s_Il2CppMethodInitialized = true;
	}
	Exception_t* ___nodeException = il2cpp_codegen_get_marshal_directive_exception("Cannot marshal field '%s' of type '%s': Reference type field marshaling is not supported.", DebuggerDisplayXmlNodeProxy_t75382D048931DB9A7D8EFB35A56EEC1D8241FB07____node_FieldInfo_var, DebuggerDisplayXmlNodeProxy_t75382D048931DB9A7D8EFB35A56EEC1D8241FB07_0_0_0_var);
	IL2CPP_RAISE_MANAGED_EXCEPTION(___nodeException, NULL);
}
IL2CPP_EXTERN_C void DebuggerDisplayXmlNodeProxy_t75382D048931DB9A7D8EFB35A56EEC1D8241FB07_marshal_pinvoke_back(const DebuggerDisplayXmlNodeProxy_t75382D048931DB9A7D8EFB35A56EEC1D8241FB07_marshaled_pinvoke& marshaled, DebuggerDisplayXmlNodeProxy_t75382D048931DB9A7D8EFB35A56EEC1D8241FB07& unmarshaled)
{
	static bool s_Il2CppMethodInitialized;
	if (!s_Il2CppMethodInitialized)
	{
		il2cpp_codegen_initialize_runtime_metadata((uintptr_t*)&DebuggerDisplayXmlNodeProxy_t75382D048931DB9A7D8EFB35A56EEC1D8241FB07_0_0_0_var);
		il2cpp_codegen_initialize_runtime_metadata((uintptr_t*)&DebuggerDisplayXmlNodeProxy_t75382D048931DB9A7D8EFB35A56EEC1D8241FB07____node_FieldInfo_var);
		s_Il2CppMethodInitialized = true;
	}
	Exception_t* ___nodeException = il2cpp_codegen_get_marshal_directive_exception("Cannot marshal field '%s' of type '%s': Reference type field marshaling is not supported.", DebuggerDisplayXmlNodeProxy_t75382D048931DB9A7D8EFB35A56EEC1D8241FB07____node_FieldInfo_var, DebuggerDisplayXmlNodeProxy_t75382D048931DB9A7D8EFB35A56EEC1D8241FB07_0_0_0_var);
	IL2CPP_RAISE_MANAGED_EXCEPTION(___nodeException, NULL);
}
IL2CPP_EXTERN_C void DebuggerDisplayXmlNodeProxy_t75382D048931DB9A7D8EFB35A56EEC1D8241FB07_marshal_pinvoke_cleanup(DebuggerDisplayXmlNodeProxy_t75382D048931DB9A7D8EFB35A56EEC1D8241FB07_marshaled_pinvoke& marshaled)
{
}
IL2CPP_EXTERN_C void DebuggerDisplayXmlNodeProxy_t75382D048931DB9A7D8EFB35A56EEC1D8241FB07_marshal_com(const DebuggerDisplayXmlNodeProxy_t75382D048931DB9A7D8EFB35A56EEC1D8241FB07& unmarshaled, DebuggerDisplayXmlNodeProxy_t75382D048931DB9A7D8EFB35A56EEC1D8241FB07_marshaled_com& marshaled)
{
	static bool s_Il2CppMethodInitialized;
	if (!s_Il2CppMethodInitialized)
	{
		il2cpp_codegen_initialize_runtime_metadata((uintptr_t*)&DebuggerDisplayXmlNodeProxy_t75382D048931DB9A7D8EFB35A56EEC1D8241FB07_0_0_0_var);
		il2cpp_codegen_initialize_runtime_metadata((uintptr_t*)&DebuggerDisplayXmlNodeProxy_t75382D048931DB9A7D8EFB35A56EEC1D8241FB07____node_FieldInfo_var);
		s_Il2CppMethodInitialized = true;
	}
	Exception_t* ___nodeException = il2cpp_codegen_get_marshal_directive_exception("Cannot marshal field '%s' of type '%s': Reference type field marshaling is not supported.", DebuggerDisplayXmlNodeProxy_t75382D048931DB9A7D8EFB35A56EEC1D8241FB07____node_FieldInfo_var, DebuggerDisplayXmlNodeProxy_t75382D048931DB9A7D8EFB35A56EEC1D8241FB07_0_0_0_var);
	IL2CPP_RAISE_MANAGED_EXCEPTION(___nodeException, NULL);
}
IL2CPP_EXTERN_C void DebuggerDisplayXmlNodeProxy_t75382D048931DB9A7D8EFB35A56EEC1D8241FB07_marshal_com_back(const DebuggerDisplayXmlNodeProxy_t75382D048931DB9A7D8EFB35A56EEC1D8241FB07_marshaled_com& marshaled, DebuggerDisplayXmlNodeProxy_t75382D048931DB9A7D8EFB35A56EEC1D8241FB07& unmarshaled)
{
	static bool s_Il2CppMethodInitialized;
	if (!s_Il2CppMethodInitialized)
	{
		il2cpp_codegen_initialize_runtime_metadata((uintptr_t*)&DebuggerDisplayXmlNodeProxy_t75382D048931DB9A7D8EFB35A56EEC1D8241FB07_0_0_0_var);
		il2cpp_codegen_initialize_runtime_metadata((uintptr_t*)&DebuggerDisplayXmlNodeProxy_t75382D048931DB9A7D8EFB35A56EEC1D8241FB07____node_FieldInfo_var);
		s_Il2CppMethodInitialized = true;
	}
	Exception_t* ___nodeException = il2cpp_codegen_get_marshal_directive_exception("Cannot marshal field '%s' of type '%s': Reference type field marshaling is not supported.", DebuggerDisplayXmlNodeProxy_t75382D048931DB9A7D8EFB35A56EEC1D8241FB07____node_FieldInfo_var, DebuggerDisplayXmlNodeProxy_t75382D048931DB9A7D8EFB35A56EEC1D8241FB07_0_0_0_var);
	IL2CPP_RAISE_MANAGED_EXCEPTION(___nodeException, NULL);
}
IL2CPP_EXTERN_C void DebuggerDisplayXmlNodeProxy_t75382D048931DB9A7D8EFB35A56EEC1D8241FB07_marshal_com_cleanup(DebuggerDisplayXmlNodeProxy_t75382D048931DB9A7D8EFB35A56EEC1D8241FB07_marshaled_com& marshaled)
{
}
// Method Definition Index: 93949
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR void DebuggerDisplayXmlNodeProxy__ctor_mB9B6D906F2EBADBBDBA79FE184D6860363C32356 (DebuggerDisplayXmlNodeProxy_t75382D048931DB9A7D8EFB35A56EEC1D8241FB07* __this, XmlNode_t3180B9B3D5C36CD58F5327D9F13458E3B3F030AF* ___0_node, const RuntimeMethod* method) 
{
	{
		XmlNode_t3180B9B3D5C36CD58F5327D9F13458E3B3F030AF* L_0 = ___0_node;
		__this->___node = L_0;
		Il2CppCodeGenWriteBarrier((void**)(&__this->___node), (void*)L_0);
		return;
	}
}
IL2CPP_EXTERN_C  void DebuggerDisplayXmlNodeProxy__ctor_mB9B6D906F2EBADBBDBA79FE184D6860363C32356_AdjustorThunk (RuntimeObject* __this, XmlNode_t3180B9B3D5C36CD58F5327D9F13458E3B3F030AF* ___0_node, const RuntimeMethod* method)
{
	DebuggerDisplayXmlNodeProxy_t75382D048931DB9A7D8EFB35A56EEC1D8241FB07* _thisAdjusted;
	int32_t _offset = 1;
	_thisAdjusted = reinterpret_cast<DebuggerDisplayXmlNodeProxy_t75382D048931DB9A7D8EFB35A56EEC1D8241FB07*>(__this + _offset);
	DebuggerDisplayXmlNodeProxy__ctor_mB9B6D906F2EBADBBDBA79FE184D6860363C32356_inline(_thisAdjusted, ___0_node, method);
}
// Method Definition Index: 93950
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR String_t* DebuggerDisplayXmlNodeProxy_ToString_mAE6AF3B460B62D08C1090FA5FA67BB80EB091D48 (DebuggerDisplayXmlNodeProxy_t75382D048931DB9A7D8EFB35A56EEC1D8241FB07* __this, const RuntimeMethod* method) 
{
	static bool s_Il2CppMethodInitialized;
	if (!s_Il2CppMethodInitialized)
	{
		il2cpp_codegen_initialize_runtime_metadata((uintptr_t*)&StringU5BU5D_t7674CD946EC0CE7B3AE0BE70E6EE85F2ECD9F248_il2cpp_TypeInfo_var);
		il2cpp_codegen_initialize_runtime_metadata((uintptr_t*)&XmlConvert_t5D100AFD62FF717BD4F8A6CABA9EDF7A5DF719EA_il2cpp_TypeInfo_var);
		il2cpp_codegen_initialize_runtime_metadata((uintptr_t*)&XmlDocumentType_tFD7379E45412FC2EBD65280ED3DE7A1C941A8D9E_il2cpp_TypeInfo_var);
		il2cpp_codegen_initialize_runtime_metadata((uintptr_t*)&XmlNodeType_tCA25567E42A464038FDA6BA318EC0A9627B4CB95_il2cpp_TypeInfo_var);
		il2cpp_codegen_initialize_runtime_metadata((uintptr_t*)&_stringLiteral2C444E82A2EF8AEC533AC89C8394A988DC706B88);
		il2cpp_codegen_initialize_runtime_metadata((uintptr_t*)&_stringLiteral3EF4F70676F5F18B8797A6624726002F607BB8DE);
		il2cpp_codegen_initialize_runtime_metadata((uintptr_t*)&_stringLiteral9465793DB5FEE411F38090ACC397B439BF6CC52C);
		il2cpp_codegen_initialize_runtime_metadata((uintptr_t*)&_stringLiteral99EFCE42C9A728EA7C113BD7034366462E5E09E6);
		il2cpp_codegen_initialize_runtime_metadata((uintptr_t*)&_stringLiteralB7CFE1337753D3AE49222B506750148BBC1B1149);
		il2cpp_codegen_initialize_runtime_metadata((uintptr_t*)&_stringLiteralC62C64F00567C5368CAE37F4E64E1E82FF785677);
		s_Il2CppMethodInitialized = true;
	}
	int32_t V_0 = 0;
	String_t* V_1 = NULL;
	XmlDocumentType_tFD7379E45412FC2EBD65280ED3DE7A1C941A8D9E* V_2 = NULL;
	{
		XmlNode_t3180B9B3D5C36CD58F5327D9F13458E3B3F030AF* L_0 = __this->___node;
		NullCheck(L_0);
		int32_t L_1;
		L_1 = VirtualFuncInvoker0< int32_t >::Invoke(6, L_0);
		V_0 = L_1;
		Il2CppFakeBox<int32_t> L_2(XmlNodeType_tCA25567E42A464038FDA6BA318EC0A9627B4CB95_il2cpp_TypeInfo_var, (&V_0));
		String_t* L_3;
		L_3 = Enum_ToString_m946B0B83C4470457D0FF555D862022C72BB55741((Enum_t2A1A94B24E3B776EEF4E5E485E290BB9D4D072E2*)(&L_2), NULL);
		V_1 = L_3;
		int32_t L_4 = V_0;
		switch (((int32_t)il2cpp_codegen_subtract((int32_t)L_4, 1)))
		{
			case 0:
			{
				goto IL_006b;
			}
			case 1:
			{
				goto IL_008c;
			}
			case 2:
			{
				goto IL_00da;
			}
			case 3:
			{
				goto IL_00da;
			}
			case 4:
			{
				goto IL_006b;
			}
			case 5:
			{
				goto IL_016c;
			}
			case 6:
			{
				goto IL_008c;
			}
			case 7:
			{
				goto IL_00da;
			}
			case 8:
			{
				goto IL_016c;
			}
			case 9:
			{
				goto IL_00fd;
			}
			case 10:
			{
				goto IL_016c;
			}
			case 11:
			{
				goto IL_016c;
			}
			case 12:
			{
				goto IL_00da;
			}
			case 13:
			{
				goto IL_00da;
			}
			case 14:
			{
				goto IL_016c;
			}
			case 15:
			{
				goto IL_016c;
			}
			case 16:
			{
				goto IL_00da;
			}
		}
	}
	{
		goto IL_016c;
	}

IL_006b:
	{
		String_t* L_5 = V_1;
		XmlNode_t3180B9B3D5C36CD58F5327D9F13458E3B3F030AF* L_6 = __this->___node;
		NullCheck(L_6);
		String_t* L_7;
		L_7 = VirtualFuncInvoker0< String_t* >::Invoke(4, L_6);
		String_t* L_8;
		L_8 = String_Concat_m093934F71A9B351911EE46311674ED463B180006(L_5, _stringLiteral99EFCE42C9A728EA7C113BD7034366462E5E09E6, L_7, _stringLiteralC62C64F00567C5368CAE37F4E64E1E82FF785677, NULL);
		V_1 = L_8;
		goto IL_016c;
	}

IL_008c:
	{
		StringU5BU5D_t7674CD946EC0CE7B3AE0BE70E6EE85F2ECD9F248* L_9 = (StringU5BU5D_t7674CD946EC0CE7B3AE0BE70E6EE85F2ECD9F248*)(StringU5BU5D_t7674CD946EC0CE7B3AE0BE70E6EE85F2ECD9F248*)SZArrayNew(StringU5BU5D_t7674CD946EC0CE7B3AE0BE70E6EE85F2ECD9F248_il2cpp_TypeInfo_var, (uint32_t)6);
		StringU5BU5D_t7674CD946EC0CE7B3AE0BE70E6EE85F2ECD9F248* L_10 = L_9;
		String_t* L_11 = V_1;
		NullCheck(L_10);
		(L_10)->SetAt(static_cast<il2cpp_array_size_t>(0), (String_t*)L_11);
		StringU5BU5D_t7674CD946EC0CE7B3AE0BE70E6EE85F2ECD9F248* L_12 = L_10;
		NullCheck(L_12);
		(L_12)->SetAt(static_cast<il2cpp_array_size_t>(1), (String_t*)_stringLiteral99EFCE42C9A728EA7C113BD7034366462E5E09E6);
		StringU5BU5D_t7674CD946EC0CE7B3AE0BE70E6EE85F2ECD9F248* L_13 = L_12;
		XmlNode_t3180B9B3D5C36CD58F5327D9F13458E3B3F030AF* L_14 = __this->___node;
		NullCheck(L_14);
		String_t* L_15;
		L_15 = VirtualFuncInvoker0< String_t* >::Invoke(4, L_14);
		NullCheck(L_13);
		(L_13)->SetAt(static_cast<il2cpp_array_size_t>(2), (String_t*)L_15);
		StringU5BU5D_t7674CD946EC0CE7B3AE0BE70E6EE85F2ECD9F248* L_16 = L_13;
		NullCheck(L_16);
		(L_16)->SetAt(static_cast<il2cpp_array_size_t>(3), (String_t*)_stringLiteral3EF4F70676F5F18B8797A6624726002F607BB8DE);
		StringU5BU5D_t7674CD946EC0CE7B3AE0BE70E6EE85F2ECD9F248* L_17 = L_16;
		XmlNode_t3180B9B3D5C36CD58F5327D9F13458E3B3F030AF* L_18 = __this->___node;
		NullCheck(L_18);
		String_t* L_19;
		L_19 = VirtualFuncInvoker0< String_t* >::Invoke(5, L_18);
		il2cpp_codegen_runtime_class_init_inline(XmlConvert_t5D100AFD62FF717BD4F8A6CABA9EDF7A5DF719EA_il2cpp_TypeInfo_var);
		String_t* L_20;
		L_20 = XmlConvert_EscapeValueForDebuggerDisplay_m4290160B46B7BA2841826618DBBD1403371CA202(L_19, NULL);
		NullCheck(L_17);
		(L_17)->SetAt(static_cast<il2cpp_array_size_t>(4), (String_t*)L_20);
		StringU5BU5D_t7674CD946EC0CE7B3AE0BE70E6EE85F2ECD9F248* L_21 = L_17;
		NullCheck(L_21);
		(L_21)->SetAt(static_cast<il2cpp_array_size_t>(5), (String_t*)_stringLiteralC62C64F00567C5368CAE37F4E64E1E82FF785677);
		String_t* L_22;
		L_22 = String_Concat_m647EBF831F54B6DF7D5AFA5FD012CF4EE7571B6A(L_21, NULL);
		V_1 = L_22;
		goto IL_016c;
	}

IL_00da:
	{
		String_t* L_23 = V_1;
		XmlNode_t3180B9B3D5C36CD58F5327D9F13458E3B3F030AF* L_24 = __this->___node;
		NullCheck(L_24);
		String_t* L_25;
		L_25 = VirtualFuncInvoker0< String_t* >::Invoke(5, L_24);
		il2cpp_codegen_runtime_class_init_inline(XmlConvert_t5D100AFD62FF717BD4F8A6CABA9EDF7A5DF719EA_il2cpp_TypeInfo_var);
		String_t* L_26;
		L_26 = XmlConvert_EscapeValueForDebuggerDisplay_m4290160B46B7BA2841826618DBBD1403371CA202(L_25, NULL);
		String_t* L_27;
		L_27 = String_Concat_m093934F71A9B351911EE46311674ED463B180006(L_23, _stringLiteral2C444E82A2EF8AEC533AC89C8394A988DC706B88, L_26, _stringLiteralC62C64F00567C5368CAE37F4E64E1E82FF785677, NULL);
		V_1 = L_27;
		goto IL_016c;
	}

IL_00fd:
	{
		XmlNode_t3180B9B3D5C36CD58F5327D9F13458E3B3F030AF* L_28 = __this->___node;
		V_2 = ((XmlDocumentType_tFD7379E45412FC2EBD65280ED3DE7A1C941A8D9E*)CastclassClass((RuntimeObject*)L_28, XmlDocumentType_tFD7379E45412FC2EBD65280ED3DE7A1C941A8D9E_il2cpp_TypeInfo_var));
		StringU5BU5D_t7674CD946EC0CE7B3AE0BE70E6EE85F2ECD9F248* L_29 = (StringU5BU5D_t7674CD946EC0CE7B3AE0BE70E6EE85F2ECD9F248*)(StringU5BU5D_t7674CD946EC0CE7B3AE0BE70E6EE85F2ECD9F248*)SZArrayNew(StringU5BU5D_t7674CD946EC0CE7B3AE0BE70E6EE85F2ECD9F248_il2cpp_TypeInfo_var, (uint32_t)((int32_t)10));
		StringU5BU5D_t7674CD946EC0CE7B3AE0BE70E6EE85F2ECD9F248* L_30 = L_29;
		String_t* L_31 = V_1;
		NullCheck(L_30);
		(L_30)->SetAt(static_cast<il2cpp_array_size_t>(0), (String_t*)L_31);
		StringU5BU5D_t7674CD946EC0CE7B3AE0BE70E6EE85F2ECD9F248* L_32 = L_30;
		NullCheck(L_32);
		(L_32)->SetAt(static_cast<il2cpp_array_size_t>(1), (String_t*)_stringLiteral99EFCE42C9A728EA7C113BD7034366462E5E09E6);
		StringU5BU5D_t7674CD946EC0CE7B3AE0BE70E6EE85F2ECD9F248* L_33 = L_32;
		XmlDocumentType_tFD7379E45412FC2EBD65280ED3DE7A1C941A8D9E* L_34 = V_2;
		NullCheck(L_34);
		String_t* L_35;
		L_35 = VirtualFuncInvoker0< String_t* >::Invoke(4, L_34);
		NullCheck(L_33);
		(L_33)->SetAt(static_cast<il2cpp_array_size_t>(2), (String_t*)L_35);
		StringU5BU5D_t7674CD946EC0CE7B3AE0BE70E6EE85F2ECD9F248* L_36 = L_33;
		NullCheck(L_36);
		(L_36)->SetAt(static_cast<il2cpp_array_size_t>(3), (String_t*)_stringLiteralB7CFE1337753D3AE49222B506750148BBC1B1149);
		StringU5BU5D_t7674CD946EC0CE7B3AE0BE70E6EE85F2ECD9F248* L_37 = L_36;
		XmlDocumentType_tFD7379E45412FC2EBD65280ED3DE7A1C941A8D9E* L_38 = V_2;
		NullCheck(L_38);
		String_t* L_39;
		L_39 = XmlDocumentType_get_SystemId_m17B225C509C0F5A4ECBB838AA81D15DED4016909_inline(L_38, NULL);
		NullCheck(L_37);
		(L_37)->SetAt(static_cast<il2cpp_array_size_t>(4), (String_t*)L_39);
		StringU5BU5D_t7674CD946EC0CE7B3AE0BE70E6EE85F2ECD9F248* L_40 = L_37;
		NullCheck(L_40);
		(L_40)->SetAt(static_cast<il2cpp_array_size_t>(5), (String_t*)_stringLiteral9465793DB5FEE411F38090ACC397B439BF6CC52C);
		StringU5BU5D_t7674CD946EC0CE7B3AE0BE70E6EE85F2ECD9F248* L_41 = L_40;
		XmlDocumentType_tFD7379E45412FC2EBD65280ED3DE7A1C941A8D9E* L_42 = V_2;
		NullCheck(L_42);
		String_t* L_43;
		L_43 = XmlDocumentType_get_PublicId_m9E81FAF2D0AA9DC847D118813CBEB085F0CE1955_inline(L_42, NULL);
		NullCheck(L_41);
		(L_41)->SetAt(static_cast<il2cpp_array_size_t>(6), (String_t*)L_43);
		StringU5BU5D_t7674CD946EC0CE7B3AE0BE70E6EE85F2ECD9F248* L_44 = L_41;
		NullCheck(L_44);
		(L_44)->SetAt(static_cast<il2cpp_array_size_t>(7), (String_t*)_stringLiteral3EF4F70676F5F18B8797A6624726002F607BB8DE);
		StringU5BU5D_t7674CD946EC0CE7B3AE0BE70E6EE85F2ECD9F248* L_45 = L_44;
		XmlDocumentType_tFD7379E45412FC2EBD65280ED3DE7A1C941A8D9E* L_46 = V_2;
		NullCheck(L_46);
		String_t* L_47;
		L_47 = XmlDocumentType_get_InternalSubset_mBD8D585ED0401EE1FC02F6FD5445E2109B083FEF_inline(L_46, NULL);
		il2cpp_codegen_runtime_class_init_inline(XmlConvert_t5D100AFD62FF717BD4F8A6CABA9EDF7A5DF719EA_il2cpp_TypeInfo_var);
		String_t* L_48;
		L_48 = XmlConvert_EscapeValueForDebuggerDisplay_m4290160B46B7BA2841826618DBBD1403371CA202(L_47, NULL);
		NullCheck(L_45);
		(L_45)->SetAt(static_cast<il2cpp_array_size_t>(8), (String_t*)L_48);
		StringU5BU5D_t7674CD946EC0CE7B3AE0BE70E6EE85F2ECD9F248* L_49 = L_45;
		NullCheck(L_49);
		(L_49)->SetAt(static_cast<il2cpp_array_size_t>(((int32_t)9)), (String_t*)_stringLiteralC62C64F00567C5368CAE37F4E64E1E82FF785677);
		String_t* L_50;
		L_50 = String_Concat_m647EBF831F54B6DF7D5AFA5FD012CF4EE7571B6A(L_49, NULL);
		V_1 = L_50;
	}

IL_016c:
	{
		String_t* L_51 = V_1;
		return L_51;
	}
}
IL2CPP_EXTERN_C  String_t* DebuggerDisplayXmlNodeProxy_ToString_mAE6AF3B460B62D08C1090FA5FA67BB80EB091D48_AdjustorThunk (RuntimeObject* __this, const RuntimeMethod* method)
{
	DebuggerDisplayXmlNodeProxy_t75382D048931DB9A7D8EFB35A56EEC1D8241FB07* _thisAdjusted;
	int32_t _offset = 1;
	_thisAdjusted = reinterpret_cast<DebuggerDisplayXmlNodeProxy_t75382D048931DB9A7D8EFB35A56EEC1D8241FB07*>(__this + _offset);
	String_t* _returnValue;
	_returnValue = DebuggerDisplayXmlNodeProxy_ToString_mAE6AF3B460B62D08C1090FA5FA67BB80EB091D48(_thisAdjusted, method);
	return _returnValue;
}
#ifdef __clang__
#pragma clang diagnostic pop
#endif
#ifdef __clang__
#pragma clang diagnostic push
#pragma clang diagnostic ignored "-Winvalid-offsetof"
#pragma clang diagnostic ignored "-Wunused-variable"
#endif
// Method Definition Index: 93951
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR int32_t XmlNodeChangedEventArgs_get_Action_mD307B00A56DE5422A4EE4915400EC966DF80BD04 (XmlNodeChangedEventArgs_t965F2E02F8433933D197A0D1AF1EFEEDB524DF3A* __this, const RuntimeMethod* method) 
{
	{
		int32_t L_0 = __this->___action;
		return L_0;
	}
}
// Method Definition Index: 93952
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR XmlNode_t3180B9B3D5C36CD58F5327D9F13458E3B3F030AF* XmlNodeChangedEventArgs_get_Node_mB5FA92B160E0200CF56DFCBAE3FC40F0D5C9B138 (XmlNodeChangedEventArgs_t965F2E02F8433933D197A0D1AF1EFEEDB524DF3A* __this, const RuntimeMethod* method) 
{
	{
		XmlNode_t3180B9B3D5C36CD58F5327D9F13458E3B3F030AF* L_0 = __this->___node;
		return L_0;
	}
}
#ifdef __clang__
#pragma clang diagnostic pop
#endif
#ifdef __clang__
#pragma clang diagnostic push
#pragma clang diagnostic ignored "-Winvalid-offsetof"
#pragma clang diagnostic ignored "-Wunused-variable"
#endif
void XmlNodeChangedEventHandler_Invoke_m64C7CF87945A0BAEFCED39FC27B1D9F2E31C1480_Multicast(XmlNodeChangedEventHandler_t907F6E9CB8DE238635028EC468AD41CAB0BA269B* __this, RuntimeObject* ___0_sender, XmlNodeChangedEventArgs_t965F2E02F8433933D197A0D1AF1EFEEDB524DF3A* ___1_e, const RuntimeMethod* method)
{
	il2cpp_array_size_t length = __this->___delegates->max_length;
	Delegate_t** delegatesToInvoke = reinterpret_cast<Delegate_t**>(__this->___delegates->GetAddressAtUnchecked(0));
	for (il2cpp_array_size_t i = 0; i < length; i++)
	{
		XmlNodeChangedEventHandler_t907F6E9CB8DE238635028EC468AD41CAB0BA269B* currentDelegate = reinterpret_cast<XmlNodeChangedEventHandler_t907F6E9CB8DE238635028EC468AD41CAB0BA269B*>(delegatesToInvoke[i]);
		typedef void (*FunctionPointerType) (RuntimeObject*, RuntimeObject*, XmlNodeChangedEventArgs_t965F2E02F8433933D197A0D1AF1EFEEDB524DF3A*, const RuntimeMethod*);
		((FunctionPointerType)currentDelegate->___invoke_impl)((Il2CppObject*)currentDelegate->___method_code, ___0_sender, ___1_e, reinterpret_cast<RuntimeMethod*>(currentDelegate->___method));
	}
}
void XmlNodeChangedEventHandler_Invoke_m64C7CF87945A0BAEFCED39FC27B1D9F2E31C1480_OpenInst(XmlNodeChangedEventHandler_t907F6E9CB8DE238635028EC468AD41CAB0BA269B* __this, RuntimeObject* ___0_sender, XmlNodeChangedEventArgs_t965F2E02F8433933D197A0D1AF1EFEEDB524DF3A* ___1_e, const RuntimeMethod* method)
{
	NullCheck(___0_sender);
	typedef void (*FunctionPointerType) (RuntimeObject*, XmlNodeChangedEventArgs_t965F2E02F8433933D197A0D1AF1EFEEDB524DF3A*, const RuntimeMethod*);
	((FunctionPointerType)__this->___method_ptr)(___0_sender, ___1_e, method);
}
void XmlNodeChangedEventHandler_Invoke_m64C7CF87945A0BAEFCED39FC27B1D9F2E31C1480_OpenStatic(XmlNodeChangedEventHandler_t907F6E9CB8DE238635028EC468AD41CAB0BA269B* __this, RuntimeObject* ___0_sender, XmlNodeChangedEventArgs_t965F2E02F8433933D197A0D1AF1EFEEDB524DF3A* ___1_e, const RuntimeMethod* method)
{
	typedef void (*FunctionPointerType) (RuntimeObject*, XmlNodeChangedEventArgs_t965F2E02F8433933D197A0D1AF1EFEEDB524DF3A*, const RuntimeMethod*);
	((FunctionPointerType)__this->___method_ptr)(___0_sender, ___1_e, method);
}
void XmlNodeChangedEventHandler_Invoke_m64C7CF87945A0BAEFCED39FC27B1D9F2E31C1480_OpenVirtual(XmlNodeChangedEventHandler_t907F6E9CB8DE238635028EC468AD41CAB0BA269B* __this, RuntimeObject* ___0_sender, XmlNodeChangedEventArgs_t965F2E02F8433933D197A0D1AF1EFEEDB524DF3A* ___1_e, const RuntimeMethod* method)
{
	NullCheck(___0_sender);
	VirtualActionInvoker1< XmlNodeChangedEventArgs_t965F2E02F8433933D197A0D1AF1EFEEDB524DF3A* >::Invoke(il2cpp_codegen_method_get_slot(method), ___0_sender, ___1_e);
}
void XmlNodeChangedEventHandler_Invoke_m64C7CF87945A0BAEFCED39FC27B1D9F2E31C1480_OpenInterface(XmlNodeChangedEventHandler_t907F6E9CB8DE238635028EC468AD41CAB0BA269B* __this, RuntimeObject* ___0_sender, XmlNodeChangedEventArgs_t965F2E02F8433933D197A0D1AF1EFEEDB524DF3A* ___1_e, const RuntimeMethod* method)
{
	NullCheck(___0_sender);
	InterfaceActionInvoker1< XmlNodeChangedEventArgs_t965F2E02F8433933D197A0D1AF1EFEEDB524DF3A* >::Invoke(il2cpp_codegen_method_get_slot(method), il2cpp_codegen_method_get_declaring_type(method), ___0_sender, ___1_e);
}
void XmlNodeChangedEventHandler_Invoke_m64C7CF87945A0BAEFCED39FC27B1D9F2E31C1480_OpenGenericVirtual(XmlNodeChangedEventHandler_t907F6E9CB8DE238635028EC468AD41CAB0BA269B* __this, RuntimeObject* ___0_sender, XmlNodeChangedEventArgs_t965F2E02F8433933D197A0D1AF1EFEEDB524DF3A* ___1_e, const RuntimeMethod* method)
{
	NullCheck(___0_sender);
	GenericVirtualActionInvoker1< XmlNodeChangedEventArgs_t965F2E02F8433933D197A0D1AF1EFEEDB524DF3A* >::Invoke(method, ___0_sender, ___1_e);
}
void XmlNodeChangedEventHandler_Invoke_m64C7CF87945A0BAEFCED39FC27B1D9F2E31C1480_OpenGenericInterface(XmlNodeChangedEventHandler_t907F6E9CB8DE238635028EC468AD41CAB0BA269B* __this, RuntimeObject* ___0_sender, XmlNodeChangedEventArgs_t965F2E02F8433933D197A0D1AF1EFEEDB524DF3A* ___1_e, const RuntimeMethod* method)
{
	NullCheck(___0_sender);
	GenericInterfaceActionInvoker1< XmlNodeChangedEventArgs_t965F2E02F8433933D197A0D1AF1EFEEDB524DF3A* >::Invoke(method, ___0_sender, ___1_e);
}
// Method Definition Index: 93953
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR void XmlNodeChangedEventHandler__ctor_mD51AE5833F8A9B1EA9DB8DC54FEF3AD64763AED9 (XmlNodeChangedEventHandler_t907F6E9CB8DE238635028EC468AD41CAB0BA269B* __this, RuntimeObject* ___0_object, intptr_t ___1_method, const RuntimeMethod* method) 
{
	__this->___method_ptr = (intptr_t)il2cpp_codegen_get_method_pointer((RuntimeMethod*)___1_method);
	__this->___method = ___1_method;
	__this->___m_target = ___0_object;
	Il2CppCodeGenWriteBarrier((void**)(&__this->___m_target), (void*)___0_object);
	int parameterCount = il2cpp_codegen_method_parameter_count((RuntimeMethod*)___1_method);
	__this->___method_code = (intptr_t)__this;
	if (MethodIsStatic((RuntimeMethod*)___1_method))
	{
		bool isOpen = parameterCount == 2;
		if (isOpen)
			__this->___invoke_impl = (intptr_t)&XmlNodeChangedEventHandler_Invoke_m64C7CF87945A0BAEFCED39FC27B1D9F2E31C1480_OpenStatic;
		else
			{
				__this->___invoke_impl = __this->___method_ptr;
				__this->___method_code = (intptr_t)__this->___m_target;
			}
	}
	else
	{
		bool isOpen = parameterCount == 1;
		if (isOpen)
		{
			if (__this->___method_is_virtual)
			{
				if (il2cpp_codegen_method_is_generic_instance_method((RuntimeMethod*)___1_method))
					if (il2cpp_codegen_method_is_interface_method((RuntimeMethod*)___1_method))
						__this->___invoke_impl = (intptr_t)&XmlNodeChangedEventHandler_Invoke_m64C7CF87945A0BAEFCED39FC27B1D9F2E31C1480_OpenGenericInterface;
					else
						__this->___invoke_impl = (intptr_t)&XmlNodeChangedEventHandler_Invoke_m64C7CF87945A0BAEFCED39FC27B1D9F2E31C1480_OpenGenericVirtual;
				else
					if (il2cpp_codegen_method_is_interface_method((RuntimeMethod*)___1_method))
						__this->___invoke_impl = (intptr_t)&XmlNodeChangedEventHandler_Invoke_m64C7CF87945A0BAEFCED39FC27B1D9F2E31C1480_OpenInterface;
					else
						__this->___invoke_impl = (intptr_t)&XmlNodeChangedEventHandler_Invoke_m64C7CF87945A0BAEFCED39FC27B1D9F2E31C1480_OpenVirtual;
			}
			else
			{
				__this->___invoke_impl = (intptr_t)&XmlNodeChangedEventHandler_Invoke_m64C7CF87945A0BAEFCED39FC27B1D9F2E31C1480_OpenInst;
			}
		}
		else
		{
			if (___0_object == NULL)
				il2cpp_codegen_raise_exception(il2cpp_codegen_get_argument_exception(NULL, "Delegate to an instance method cannot have null 'this'."), NULL);
			__this->___invoke_impl = __this->___method_ptr;
			__this->___method_code = (intptr_t)__this->___m_target;
		}
	}
	__this->___extra_arg = (intptr_t)&XmlNodeChangedEventHandler_Invoke_m64C7CF87945A0BAEFCED39FC27B1D9F2E31C1480_Multicast;
}
// Method Definition Index: 93954
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR void XmlNodeChangedEventHandler_Invoke_m64C7CF87945A0BAEFCED39FC27B1D9F2E31C1480 (XmlNodeChangedEventHandler_t907F6E9CB8DE238635028EC468AD41CAB0BA269B* __this, RuntimeObject* ___0_sender, XmlNodeChangedEventArgs_t965F2E02F8433933D197A0D1AF1EFEEDB524DF3A* ___1_e, const RuntimeMethod* method) 
{
	typedef void (*FunctionPointerType) (RuntimeObject*, RuntimeObject*, XmlNodeChangedEventArgs_t965F2E02F8433933D197A0D1AF1EFEEDB524DF3A*, const RuntimeMethod*);
	((FunctionPointerType)__this->___invoke_impl)((Il2CppObject*)__this->___method_code, ___0_sender, ___1_e, reinterpret_cast<RuntimeMethod*>(__this->___method));
}
#ifdef __clang__
#pragma clang diagnostic pop
#endif
#ifdef __clang__
#pragma clang diagnostic push
#pragma clang diagnostic ignored "-Winvalid-offsetof"
#pragma clang diagnostic ignored "-Wunused-variable"
#endif
// Method Definition Index: 93956
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR void XmlNodeList_System_IDisposable_Dispose_m755D56C61D93F597B8AFC6FB8BE9677A1973BFA0 (XmlNodeList_tFC0CEED3A006BEDF6A1A420F7CB55AC33E9B7F85* __this, const RuntimeMethod* method) 
{
	{
		VirtualActionInvoker0::Invoke(7, __this);
		return;
	}
}
// Method Definition Index: 93957
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR void XmlNodeList_PrivateDisposeNodeList_m3B13AE65E266052E2824582D77B44124F6C1E520 (XmlNodeList_tFC0CEED3A006BEDF6A1A420F7CB55AC33E9B7F85* __this, const RuntimeMethod* method) 
{
	{
		return;
	}
}
// Method Definition Index: 93958
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR void XmlNodeList__ctor_m29940F85BF8D20E6FFB9732648DC6DAD10539BD3 (XmlNodeList_tFC0CEED3A006BEDF6A1A420F7CB55AC33E9B7F85* __this, const RuntimeMethod* method) 
{
	{
		Object__ctor_mE837C6B9FA8C6D5D109F4B2EC885D79919AC0EA2(__this, NULL);
		return;
	}
}
#ifdef __clang__
#pragma clang diagnostic pop
#endif
#ifdef __clang__
#pragma clang diagnostic push
#pragma clang diagnostic ignored "-Winvalid-offsetof"
#pragma clang diagnostic ignored "-Wunused-variable"
#endif
// Method Definition Index: 93959
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR bool EmptyEnumerator_System_Collections_IEnumerator_MoveNext_m1D1ED329E1A11EB09BF11A969309A8C336363E06 (EmptyEnumerator_t84EC9187C8460EE98E675ED9258AE4DF2A6776DA* __this, const RuntimeMethod* method) 
{
	{
		return (bool)0;
	}
}
// Method Definition Index: 93960
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR void EmptyEnumerator_System_Collections_IEnumerator_Reset_m60B63D059F4ABACCFF0136AD35A7B858CDC6DAD4 (EmptyEnumerator_t84EC9187C8460EE98E675ED9258AE4DF2A6776DA* __this, const RuntimeMethod* method) 
{
	{
		return;
	}
}
// Method Definition Index: 93961
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR RuntimeObject* EmptyEnumerator_System_Collections_IEnumerator_get_Current_m7F54B324E8E3410019E56521EE61CF97A7CED9B5 (EmptyEnumerator_t84EC9187C8460EE98E675ED9258AE4DF2A6776DA* __this, const RuntimeMethod* method) 
{
	{
		String_t* L_0;
		L_0 = Res_GetString_mBEE82ACFE97F93A6DDE81E322477CF2713466364(((String_t*)il2cpp_codegen_initialize_runtime_metadata_inline((uintptr_t*)&_stringLiteralFBD1A9745DCC4D80697D44756437CCB57698AE21)), NULL);
		InvalidOperationException_t5DDE4D49B7405FAAB1E4576F4715A42A3FAD4BAB* L_1 = (InvalidOperationException_t5DDE4D49B7405FAAB1E4576F4715A42A3FAD4BAB*)il2cpp_codegen_object_new(((RuntimeClass*)il2cpp_codegen_initialize_runtime_metadata_inline((uintptr_t*)&InvalidOperationException_t5DDE4D49B7405FAAB1E4576F4715A42A3FAD4BAB_il2cpp_TypeInfo_var)));
		InvalidOperationException__ctor_mE4CB6F4712AB6D99A2358FBAE2E052B3EE976162(L_1, L_0, NULL);
		IL2CPP_RAISE_MANAGED_EXCEPTION(L_1, ((RuntimeMethod*)il2cpp_codegen_initialize_runtime_metadata_inline((uintptr_t*)&EmptyEnumerator_System_Collections_IEnumerator_get_Current_m7F54B324E8E3410019E56521EE61CF97A7CED9B5_RuntimeMethod_var)));
	}
}
// Method Definition Index: 93962
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR void EmptyEnumerator__ctor_m05DA73C42354A4E549CCA7E9485B294057ABA981 (EmptyEnumerator_t84EC9187C8460EE98E675ED9258AE4DF2A6776DA* __this, const RuntimeMethod* method) 
{
	{
		Object__ctor_mE837C6B9FA8C6D5D109F4B2EC885D79919AC0EA2(__this, NULL);
		return;
	}
}
#ifdef __clang__
#pragma clang diagnostic pop
#endif
#ifdef __clang__
#pragma clang diagnostic push
#pragma clang diagnostic ignored "-Winvalid-offsetof"
#pragma clang diagnostic ignored "-Wunused-variable"
#endif
// Method Definition Index: 93963
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR bool Ref_Equal_m60FD3EF02546279815B785F856568B01F60FE1AF (String_t* ___0_strA, String_t* ___1_strB, const RuntimeMethod* method) 
{
	{
		String_t* L_0 = ___0_strA;
		String_t* L_1 = ___1_strB;
		return (bool)((((RuntimeObject*)(String_t*)L_0) == ((RuntimeObject*)(String_t*)L_1))? 1 : 0);
	}
}
#ifdef __clang__
#pragma clang diagnostic pop
#endif
#ifdef __clang__
#pragma clang diagnostic push
#pragma clang diagnostic ignored "-Winvalid-offsetof"
#pragma clang diagnostic ignored "-Wunused-variable"
#endif
IL2CPP_EXTERN_C void XmlCharType_t7C471894C8862126737E800F5A14AACBD14FCBC3_marshal_pinvoke(const XmlCharType_t7C471894C8862126737E800F5A14AACBD14FCBC3& unmarshaled, XmlCharType_t7C471894C8862126737E800F5A14AACBD14FCBC3_marshaled_pinvoke& marshaled)
{
	marshaled.___charProperties = il2cpp_codegen_com_marshal_safe_array(IL2CPP_VT_I1, unmarshaled.___charProperties);
}
IL2CPP_EXTERN_C void XmlCharType_t7C471894C8862126737E800F5A14AACBD14FCBC3_marshal_pinvoke_back(const XmlCharType_t7C471894C8862126737E800F5A14AACBD14FCBC3_marshaled_pinvoke& marshaled, XmlCharType_t7C471894C8862126737E800F5A14AACBD14FCBC3& unmarshaled)
{
	unmarshaled.___charProperties = (ByteU5BU5D_tA6237BF417AE52AD70CFB4EF24A7A82613DF9031*)il2cpp_codegen_com_marshal_safe_array_result(IL2CPP_VT_I1, il2cpp_defaults.byte_class, marshaled.___charProperties);
	Il2CppCodeGenWriteBarrier((void**)(&unmarshaled.___charProperties), (void*)(ByteU5BU5D_tA6237BF417AE52AD70CFB4EF24A7A82613DF9031*)il2cpp_codegen_com_marshal_safe_array_result(IL2CPP_VT_I1, il2cpp_defaults.byte_class, marshaled.___charProperties));
}
IL2CPP_EXTERN_C void XmlCharType_t7C471894C8862126737E800F5A14AACBD14FCBC3_marshal_pinvoke_cleanup(XmlCharType_t7C471894C8862126737E800F5A14AACBD14FCBC3_marshaled_pinvoke& marshaled)
{
	il2cpp_codegen_com_destroy_safe_array(marshaled.___charProperties);
	marshaled.___charProperties = NULL;
}
IL2CPP_EXTERN_C void XmlCharType_t7C471894C8862126737E800F5A14AACBD14FCBC3_marshal_com(const XmlCharType_t7C471894C8862126737E800F5A14AACBD14FCBC3& unmarshaled, XmlCharType_t7C471894C8862126737E800F5A14AACBD14FCBC3_marshaled_com& marshaled)
{
	marshaled.___charProperties = il2cpp_codegen_com_marshal_safe_array(IL2CPP_VT_I1, unmarshaled.___charProperties);
}
IL2CPP_EXTERN_C void XmlCharType_t7C471894C8862126737E800F5A14AACBD14FCBC3_marshal_com_back(const XmlCharType_t7C471894C8862126737E800F5A14AACBD14FCBC3_marshaled_com& marshaled, XmlCharType_t7C471894C8862126737E800F5A14AACBD14FCBC3& unmarshaled)
{
	unmarshaled.___charProperties = (ByteU5BU5D_tA6237BF417AE52AD70CFB4EF24A7A82613DF9031*)il2cpp_codegen_com_marshal_safe_array_result(IL2CPP_VT_I1, il2cpp_defaults.byte_class, marshaled.___charProperties);
	Il2CppCodeGenWriteBarrier((void**)(&unmarshaled.___charProperties), (void*)(ByteU5BU5D_tA6237BF417AE52AD70CFB4EF24A7A82613DF9031*)il2cpp_codegen_com_marshal_safe_array_result(IL2CPP_VT_I1, il2cpp_defaults.byte_class, marshaled.___charProperties));
}
IL2CPP_EXTERN_C void XmlCharType_t7C471894C8862126737E800F5A14AACBD14FCBC3_marshal_com_cleanup(XmlCharType_t7C471894C8862126737E800F5A14AACBD14FCBC3_marshaled_com& marshaled)
{
	il2cpp_codegen_com_destroy_safe_array(marshaled.___charProperties);
	marshaled.___charProperties = NULL;
}
// Method Definition Index: 93964
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR RuntimeObject* XmlCharType_get_StaticLock_m39193568D86AD7E7A5AA4B3745D64C6F5D60472E (const RuntimeMethod* method) 
{
	static bool s_Il2CppMethodInitialized;
	if (!s_Il2CppMethodInitialized)
	{
		il2cpp_codegen_initialize_runtime_metadata((uintptr_t*)&RuntimeObject_il2cpp_TypeInfo_var);
		il2cpp_codegen_initialize_runtime_metadata((uintptr_t*)&XmlCharType_t7C471894C8862126737E800F5A14AACBD14FCBC3_il2cpp_TypeInfo_var);
		s_Il2CppMethodInitialized = true;
	}
	RuntimeObject* V_0 = NULL;
	{
		RuntimeObject* L_0 = ((XmlCharType_t7C471894C8862126737E800F5A14AACBD14FCBC3_StaticFields*)il2cpp_codegen_static_fields_for(XmlCharType_t7C471894C8862126737E800F5A14AACBD14FCBC3_il2cpp_TypeInfo_var))->___s_Lock;
		if (L_0)
		{
			goto IL_001a;
		}
	}
	{
		RuntimeObject* L_1 = (RuntimeObject*)il2cpp_codegen_object_new(RuntimeObject_il2cpp_TypeInfo_var);
		Object__ctor_mE837C6B9FA8C6D5D109F4B2EC885D79919AC0EA2(L_1, NULL);
		V_0 = L_1;
		RuntimeObject* L_2 = V_0;
		RuntimeObject* L_3;
		L_3 = InterlockedCompareExchangeImpl<RuntimeObject*>((&((XmlCharType_t7C471894C8862126737E800F5A14AACBD14FCBC3_StaticFields*)il2cpp_codegen_static_fields_for(XmlCharType_t7C471894C8862126737E800F5A14AACBD14FCBC3_il2cpp_TypeInfo_var))->___s_Lock), L_2, NULL);
	}

IL_001a:
	{
		RuntimeObject* L_4 = ((XmlCharType_t7C471894C8862126737E800F5A14AACBD14FCBC3_StaticFields*)il2cpp_codegen_static_fields_for(XmlCharType_t7C471894C8862126737E800F5A14AACBD14FCBC3_il2cpp_TypeInfo_var))->___s_Lock;
		return L_4;
	}
}
// Method Definition Index: 93965
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR void XmlCharType_InitInstance_m486814EDE27EF404061078D3EA69301B23F01378 (const RuntimeMethod* method) 
{
	static bool s_Il2CppMethodInitialized;
	if (!s_Il2CppMethodInitialized)
	{
		il2cpp_codegen_initialize_runtime_metadata((uintptr_t*)&ByteU5BU5D_tA6237BF417AE52AD70CFB4EF24A7A82613DF9031_il2cpp_TypeInfo_var);
		il2cpp_codegen_initialize_runtime_metadata((uintptr_t*)&XmlCharType_t7C471894C8862126737E800F5A14AACBD14FCBC3_il2cpp_TypeInfo_var);
		il2cpp_codegen_initialize_runtime_metadata((uintptr_t*)&_stringLiteral02E5C3B8AD72F3FC46F6C829AB1FEDDC9B115473);
		il2cpp_codegen_initialize_runtime_metadata((uintptr_t*)&_stringLiteral0E0426234071F961E725D9438C6D5A048A0394B3);
		il2cpp_codegen_initialize_runtime_metadata((uintptr_t*)&_stringLiteral2309CFA4F156DCCB12A14E727DCE560E1426B532);
		il2cpp_codegen_initialize_runtime_metadata((uintptr_t*)&_stringLiteral81ABCB006928101D1882FA2FBB7BFA00FE053221);
		il2cpp_codegen_initialize_runtime_metadata((uintptr_t*)&_stringLiteral94A9AE3A92C04F7400ADC94B7BCB73FFC0A08DE4);
		il2cpp_codegen_initialize_runtime_metadata((uintptr_t*)&_stringLiteralCC5753B4554091FA687FA64F4FA303B0C913E2EC);
		il2cpp_codegen_initialize_runtime_metadata((uintptr_t*)&_stringLiteralFDA1C9397356AF77AAE64869A9D6288562F10D4C);
		s_Il2CppMethodInitialized = true;
	}
	RuntimeObject* V_0 = NULL;
	bool V_1 = false;
	{
		RuntimeObject* L_0;
		L_0 = XmlCharType_get_StaticLock_m39193568D86AD7E7A5AA4B3745D64C6F5D60472E(NULL);
		V_0 = L_0;
		V_1 = (bool)0;
	}
	{
		auto __finallyBlock = il2cpp::utils::Finally([&]
		{

FINALLY_009d:
			{
				{
					bool L_1 = V_1;
					if (!L_1)
					{
						goto IL_00a6;
					}
				}
				{
					RuntimeObject* L_2 = V_0;
					Monitor_Exit_m05B2CF037E2214B3208198C282490A2A475653FA(L_2, NULL);
				}

IL_00a6:
				{
					return;
				}
			}
		});
		try
		{
			{
				RuntimeObject* L_3 = V_0;
				Monitor_Enter_m3CDB589DA1300B513D55FDCFB52B63E879794149(L_3, (&V_1), NULL);
				ByteU5BU5D_tA6237BF417AE52AD70CFB4EF24A7A82613DF9031* L_4 = ((XmlCharType_t7C471894C8862126737E800F5A14AACBD14FCBC3_StaticFields*)il2cpp_codegen_static_fields_for(XmlCharType_t7C471894C8862126737E800F5A14AACBD14FCBC3_il2cpp_TypeInfo_var))->___s_CharProperties;
				il2cpp_codegen_memory_barrier();
				if (!L_4)
				{
					goto IL_001e_1;
				}
			}
			{
				goto IL_00a7;
			}

IL_001e_1:
			{
				ByteU5BU5D_tA6237BF417AE52AD70CFB4EF24A7A82613DF9031* L_5 = (ByteU5BU5D_tA6237BF417AE52AD70CFB4EF24A7A82613DF9031*)(ByteU5BU5D_tA6237BF417AE52AD70CFB4EF24A7A82613DF9031*)SZArrayNew(ByteU5BU5D_tA6237BF417AE52AD70CFB4EF24A7A82613DF9031_il2cpp_TypeInfo_var, (uint32_t)((int32_t)65536));
				ByteU5BU5D_tA6237BF417AE52AD70CFB4EF24A7A82613DF9031* L_6 = L_5;
				XmlCharType_SetProperties_m8964034908DAEC93686ADC1D879E15967B91D4D4(L_6, _stringLiteral2309CFA4F156DCCB12A14E727DCE560E1426B532, (uint8_t)1, NULL);
				ByteU5BU5D_tA6237BF417AE52AD70CFB4EF24A7A82613DF9031* L_7 = L_6;
				XmlCharType_SetProperties_m8964034908DAEC93686ADC1D879E15967B91D4D4(L_7, _stringLiteral0E0426234071F961E725D9438C6D5A048A0394B3, (uint8_t)2, NULL);
				ByteU5BU5D_tA6237BF417AE52AD70CFB4EF24A7A82613DF9031* L_8 = L_7;
				XmlCharType_SetProperties_m8964034908DAEC93686ADC1D879E15967B91D4D4(L_8, _stringLiteralCC5753B4554091FA687FA64F4FA303B0C913E2EC, (uint8_t)4, NULL);
				ByteU5BU5D_tA6237BF417AE52AD70CFB4EF24A7A82613DF9031* L_9 = L_8;
				XmlCharType_SetProperties_m8964034908DAEC93686ADC1D879E15967B91D4D4(L_9, _stringLiteral02E5C3B8AD72F3FC46F6C829AB1FEDDC9B115473, (uint8_t)8, NULL);
				ByteU5BU5D_tA6237BF417AE52AD70CFB4EF24A7A82613DF9031* L_10 = L_9;
				XmlCharType_SetProperties_m8964034908DAEC93686ADC1D879E15967B91D4D4(L_10, _stringLiteralFDA1C9397356AF77AAE64869A9D6288562F10D4C, (uint8_t)((int32_t)16), NULL);
				ByteU5BU5D_tA6237BF417AE52AD70CFB4EF24A7A82613DF9031* L_11 = L_10;
				XmlCharType_SetProperties_m8964034908DAEC93686ADC1D879E15967B91D4D4(L_11, _stringLiteral02E5C3B8AD72F3FC46F6C829AB1FEDDC9B115473, (uint8_t)((int32_t)32), NULL);
				ByteU5BU5D_tA6237BF417AE52AD70CFB4EF24A7A82613DF9031* L_12 = L_11;
				XmlCharType_SetProperties_m8964034908DAEC93686ADC1D879E15967B91D4D4(L_12, _stringLiteral94A9AE3A92C04F7400ADC94B7BCB73FFC0A08DE4, (uint8_t)((int32_t)64), NULL);
				ByteU5BU5D_tA6237BF417AE52AD70CFB4EF24A7A82613DF9031* L_13 = L_12;
				XmlCharType_SetProperties_m8964034908DAEC93686ADC1D879E15967B91D4D4(L_13, _stringLiteral81ABCB006928101D1882FA2FBB7BFA00FE053221, (uint8_t)((int32_t)128), NULL);
				Thread_MemoryBarrier_m83873F1E6CEB16C0781941141382DA874A36097D(NULL);
				il2cpp_codegen_memory_barrier();
				((XmlCharType_t7C471894C8862126737E800F5A14AACBD14FCBC3_StaticFields*)il2cpp_codegen_static_fields_for(XmlCharType_t7C471894C8862126737E800F5A14AACBD14FCBC3_il2cpp_TypeInfo_var))->___s_CharProperties = L_13;
				Il2CppCodeGenWriteBarrier((void**)(&((XmlCharType_t7C471894C8862126737E800F5A14AACBD14FCBC3_StaticFields*)il2cpp_codegen_static_fields_for(XmlCharType_t7C471894C8862126737E800F5A14AACBD14FCBC3_il2cpp_TypeInfo_var))->___s_CharProperties), (void*)L_13);
				goto IL_00a7;
			}
		}
		catch(Il2CppExceptionWrapper& e)
		{
			__finallyBlock.StoreException(e.ex);
		}
	}

IL_00a7:
	{
		return;
	}
}
// Method Definition Index: 93966
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR void XmlCharType_SetProperties_m8964034908DAEC93686ADC1D879E15967B91D4D4 (ByteU5BU5D_tA6237BF417AE52AD70CFB4EF24A7A82613DF9031* ___0_chProps, String_t* ___1_ranges, uint8_t ___2_value, const RuntimeMethod* method) 
{
	int32_t V_0 = 0;
	int32_t V_1 = 0;
	int32_t V_2 = 0;
	{
		V_0 = 0;
		goto IL_0031;
	}

IL_0004:
	{
		String_t* L_0 = ___1_ranges;
		int32_t L_1 = V_0;
		NullCheck(L_0);
		Il2CppChar L_2;
		L_2 = String_get_Chars_mC49DF0CD2D3BE7BE97B3AD9C995BE3094F8E36D3(L_0, L_1, NULL);
		V_1 = L_2;
		String_t* L_3 = ___1_ranges;
		int32_t L_4 = V_0;
		NullCheck(L_3);
		Il2CppChar L_5;
		L_5 = String_get_Chars_mC49DF0CD2D3BE7BE97B3AD9C995BE3094F8E36D3(L_3, ((int32_t)il2cpp_codegen_add(L_4, 1)), NULL);
		V_2 = L_5;
		goto IL_0029;
	}

IL_0018:
	{
		ByteU5BU5D_tA6237BF417AE52AD70CFB4EF24A7A82613DF9031* L_6 = ___0_chProps;
		int32_t L_7 = V_1;
		NullCheck(L_6);
		uint8_t* L_8 = ((L_6)->GetAddressAt(static_cast<il2cpp_array_size_t>(L_7)));
		int32_t L_9 = *((uint8_t*)L_8);
		uint8_t L_10 = ___2_value;
		*((int8_t*)L_8) = (int8_t)((int32_t)(uint8_t)((int32_t)(L_9|(int32_t)L_10)));
		int32_t L_11 = V_1;
		V_1 = ((int32_t)il2cpp_codegen_add(L_11, 1));
	}

IL_0029:
	{
		int32_t L_12 = V_1;
		int32_t L_13 = V_2;
		if ((((int32_t)L_12) <= ((int32_t)L_13)))
		{
			goto IL_0018;
		}
	}
	{
		int32_t L_14 = V_0;
		V_0 = ((int32_t)il2cpp_codegen_add(L_14, 2));
	}

IL_0031:
	{
		int32_t L_15 = V_0;
		String_t* L_16 = ___1_ranges;
		NullCheck(L_16);
		int32_t L_17;
		L_17 = String_get_Length_m42625D67623FA5CC7A44D47425CE86FB946542D2_inline(L_16, NULL);
		if ((((int32_t)L_15) < ((int32_t)L_17)))
		{
			goto IL_0004;
		}
	}
	{
		return;
	}
}
// Method Definition Index: 93967
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR void XmlCharType__ctor_mB4A379AC51751894C31365E176E3B9BFB5ABC771 (XmlCharType_t7C471894C8862126737E800F5A14AACBD14FCBC3* __this, ByteU5BU5D_tA6237BF417AE52AD70CFB4EF24A7A82613DF9031* ___0_charProperties, const RuntimeMethod* method) 
{
	{
		ByteU5BU5D_tA6237BF417AE52AD70CFB4EF24A7A82613DF9031* L_0 = ___0_charProperties;
		__this->___charProperties = L_0;
		Il2CppCodeGenWriteBarrier((void**)(&__this->___charProperties), (void*)L_0);
		return;
	}
}
IL2CPP_EXTERN_C  void XmlCharType__ctor_mB4A379AC51751894C31365E176E3B9BFB5ABC771_AdjustorThunk (RuntimeObject* __this, ByteU5BU5D_tA6237BF417AE52AD70CFB4EF24A7A82613DF9031* ___0_charProperties, const RuntimeMethod* method)
{
	XmlCharType_t7C471894C8862126737E800F5A14AACBD14FCBC3* _thisAdjusted;
	int32_t _offset = 1;
	_thisAdjusted = reinterpret_cast<XmlCharType_t7C471894C8862126737E800F5A14AACBD14FCBC3*>(__this + _offset);
	XmlCharType__ctor_mB4A379AC51751894C31365E176E3B9BFB5ABC771_inline(_thisAdjusted, ___0_charProperties, method);
}
// Method Definition Index: 93968
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR XmlCharType_t7C471894C8862126737E800F5A14AACBD14FCBC3 XmlCharType_get_Instance_m1279A3FC91B74599A9BC76979E50DBFC07F67C55 (const RuntimeMethod* method) 
{
	static bool s_Il2CppMethodInitialized;
	if (!s_Il2CppMethodInitialized)
	{
		il2cpp_codegen_initialize_runtime_metadata((uintptr_t*)&XmlCharType_t7C471894C8862126737E800F5A14AACBD14FCBC3_il2cpp_TypeInfo_var);
		s_Il2CppMethodInitialized = true;
	}
	{
		ByteU5BU5D_tA6237BF417AE52AD70CFB4EF24A7A82613DF9031* L_0 = ((XmlCharType_t7C471894C8862126737E800F5A14AACBD14FCBC3_StaticFields*)il2cpp_codegen_static_fields_for(XmlCharType_t7C471894C8862126737E800F5A14AACBD14FCBC3_il2cpp_TypeInfo_var))->___s_CharProperties;
		il2cpp_codegen_memory_barrier();
		if (L_0)
		{
			goto IL_000e;
		}
	}
	{
		XmlCharType_InitInstance_m486814EDE27EF404061078D3EA69301B23F01378(NULL);
	}

IL_000e:
	{
		ByteU5BU5D_tA6237BF417AE52AD70CFB4EF24A7A82613DF9031* L_1 = ((XmlCharType_t7C471894C8862126737E800F5A14AACBD14FCBC3_StaticFields*)il2cpp_codegen_static_fields_for(XmlCharType_t7C471894C8862126737E800F5A14AACBD14FCBC3_il2cpp_TypeInfo_var))->___s_CharProperties;
		il2cpp_codegen_memory_barrier();
		XmlCharType_t7C471894C8862126737E800F5A14AACBD14FCBC3 L_2;
		memset((&L_2), 0, sizeof(L_2));
		XmlCharType__ctor_mB4A379AC51751894C31365E176E3B9BFB5ABC771_inline((&L_2), L_1, NULL);
		return L_2;
	}
}
#ifdef __clang__
#pragma clang diagnostic pop
#endif
#ifdef __clang__
#pragma clang diagnostic push
#pragma clang diagnostic ignored "-Winvalid-offsetof"
#pragma clang diagnostic ignored "-Wunused-variable"
#endif
// Method Definition Index: 93969
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR String_t* XmlConvert_EscapeValueForDebuggerDisplay_m4290160B46B7BA2841826618DBBD1403371CA202 (String_t* ___0_value, const RuntimeMethod* method) 
{
	static bool s_Il2CppMethodInitialized;
	if (!s_Il2CppMethodInitialized)
	{
		il2cpp_codegen_initialize_runtime_metadata((uintptr_t*)&StringBuilder_t_il2cpp_TypeInfo_var);
		il2cpp_codegen_initialize_runtime_metadata((uintptr_t*)&_stringLiteral785F17F45C331C415D0A7458E6AAC36966399C51);
		il2cpp_codegen_initialize_runtime_metadata((uintptr_t*)&_stringLiteral7F3238CD8C342B06FB9AB185C610175C84625462);
		il2cpp_codegen_initialize_runtime_metadata((uintptr_t*)&_stringLiteral848E5ED630B3142F565DD995C6E8D30187ED33CD);
		il2cpp_codegen_initialize_runtime_metadata((uintptr_t*)&_stringLiteralB78F235D4291950A7D101307609C259F3E1F033F);
		s_Il2CppMethodInitialized = true;
	}
	StringBuilder_t* V_0 = NULL;
	int32_t V_1 = 0;
	int32_t V_2 = 0;
	Il2CppChar V_3 = 0x0;
	{
		V_0 = (StringBuilder_t*)NULL;
		V_1 = 0;
		V_2 = 0;
		goto IL_00ad;
	}

IL_000b:
	{
		String_t* L_0 = ___0_value;
		int32_t L_1 = V_1;
		NullCheck(L_0);
		Il2CppChar L_2;
		L_2 = String_get_Chars_mC49DF0CD2D3BE7BE97B3AD9C995BE3094F8E36D3(L_0, L_1, NULL);
		V_3 = L_2;
		Il2CppChar L_3 = V_3;
		if ((((int32_t)L_3) < ((int32_t)((int32_t)32))))
		{
			goto IL_0020;
		}
	}
	{
		Il2CppChar L_4 = V_3;
		if ((!(((uint32_t)L_4) == ((uint32_t)((int32_t)34)))))
		{
			goto IL_00a9;
		}
	}

IL_0020:
	{
		StringBuilder_t* L_5 = V_0;
		if (L_5)
		{
			goto IL_0031;
		}
	}
	{
		String_t* L_6 = ___0_value;
		NullCheck(L_6);
		int32_t L_7;
		L_7 = String_get_Length_m42625D67623FA5CC7A44D47425CE86FB946542D2_inline(L_6, NULL);
		StringBuilder_t* L_8 = (StringBuilder_t*)il2cpp_codegen_object_new(StringBuilder_t_il2cpp_TypeInfo_var);
		StringBuilder__ctor_m2619CA8D2C3476DF1A302D9D941498BB1C6164C5(L_8, ((int32_t)il2cpp_codegen_add(L_7, 4)), NULL);
		V_0 = L_8;
	}

IL_0031:
	{
		int32_t L_9 = V_1;
		int32_t L_10 = V_2;
		if ((((int32_t)((int32_t)il2cpp_codegen_subtract(L_9, L_10))) <= ((int32_t)0)))
		{
			goto IL_0043;
		}
	}
	{
		StringBuilder_t* L_11 = V_0;
		String_t* L_12 = ___0_value;
		int32_t L_13 = V_2;
		int32_t L_14 = V_1;
		int32_t L_15 = V_2;
		NullCheck(L_11);
		StringBuilder_t* L_16;
		L_16 = StringBuilder_Append_mE7CA22532FEF1545B8BC357CB7CAA304BA264703(L_11, L_12, L_13, ((int32_t)il2cpp_codegen_subtract(L_14, L_15)), NULL);
	}

IL_0043:
	{
		int32_t L_17 = V_1;
		V_2 = ((int32_t)il2cpp_codegen_add(L_17, 1));
		Il2CppChar L_18 = V_3;
		switch (((int32_t)il2cpp_codegen_subtract((int32_t)L_18, ((int32_t)9))))
		{
			case 0:
			{
				goto IL_0093;
			}
			case 1:
			{
				goto IL_0085;
			}
			case 2:
			{
				goto IL_00a1;
			}
			case 3:
			{
				goto IL_00a1;
			}
			case 4:
			{
				goto IL_0077;
			}
		}
	}
	{
		Il2CppChar L_19 = V_3;
		if ((!(((uint32_t)L_19) == ((uint32_t)((int32_t)34)))))
		{
			goto IL_00a1;
		}
	}
	{
		StringBuilder_t* L_20 = V_0;
		NullCheck(L_20);
		StringBuilder_t* L_21;
		L_21 = StringBuilder_Append_m08904D74E0C78E5F36DCD9C9303BDD07886D9F7D(L_20, _stringLiteral848E5ED630B3142F565DD995C6E8D30187ED33CD, NULL);
		goto IL_00a9;
	}

IL_0077:
	{
		StringBuilder_t* L_22 = V_0;
		NullCheck(L_22);
		StringBuilder_t* L_23;
		L_23 = StringBuilder_Append_m08904D74E0C78E5F36DCD9C9303BDD07886D9F7D(L_22, _stringLiteralB78F235D4291950A7D101307609C259F3E1F033F, NULL);
		goto IL_00a9;
	}

IL_0085:
	{
		StringBuilder_t* L_24 = V_0;
		NullCheck(L_24);
		StringBuilder_t* L_25;
		L_25 = StringBuilder_Append_m08904D74E0C78E5F36DCD9C9303BDD07886D9F7D(L_24, _stringLiteral785F17F45C331C415D0A7458E6AAC36966399C51, NULL);
		goto IL_00a9;
	}

IL_0093:
	{
		StringBuilder_t* L_26 = V_0;
		NullCheck(L_26);
		StringBuilder_t* L_27;
		L_27 = StringBuilder_Append_m08904D74E0C78E5F36DCD9C9303BDD07886D9F7D(L_26, _stringLiteral7F3238CD8C342B06FB9AB185C610175C84625462, NULL);
		goto IL_00a9;
	}

IL_00a1:
	{
		StringBuilder_t* L_28 = V_0;
		Il2CppChar L_29 = V_3;
		NullCheck(L_28);
		StringBuilder_t* L_30;
		L_30 = StringBuilder_Append_m71228B30F05724CD2CD96D9611DCD61BFB96A6E1(L_28, L_29, NULL);
	}

IL_00a9:
	{
		int32_t L_31 = V_1;
		V_1 = ((int32_t)il2cpp_codegen_add(L_31, 1));
	}

IL_00ad:
	{
		int32_t L_32 = V_1;
		String_t* L_33 = ___0_value;
		NullCheck(L_33);
		int32_t L_34;
		L_34 = String_get_Length_m42625D67623FA5CC7A44D47425CE86FB946542D2_inline(L_33, NULL);
		if ((((int32_t)L_32) < ((int32_t)L_34)))
		{
			goto IL_000b;
		}
	}
	{
		StringBuilder_t* L_35 = V_0;
		if (L_35)
		{
			goto IL_00be;
		}
	}
	{
		String_t* L_36 = ___0_value;
		return L_36;
	}

IL_00be:
	{
		int32_t L_37 = V_1;
		int32_t L_38 = V_2;
		if ((((int32_t)((int32_t)il2cpp_codegen_subtract(L_37, L_38))) <= ((int32_t)0)))
		{
			goto IL_00d0;
		}
	}
	{
		StringBuilder_t* L_39 = V_0;
		String_t* L_40 = ___0_value;
		int32_t L_41 = V_2;
		int32_t L_42 = V_1;
		int32_t L_43 = V_2;
		NullCheck(L_39);
		StringBuilder_t* L_44;
		L_44 = StringBuilder_Append_mE7CA22532FEF1545B8BC357CB7CAA304BA264703(L_39, L_40, L_41, ((int32_t)il2cpp_codegen_subtract(L_42, L_43)), NULL);
	}

IL_00d0:
	{
		StringBuilder_t* L_45 = V_0;
		NullCheck(L_45);
		String_t* L_46;
		L_46 = VirtualFuncInvoker0< String_t* >::Invoke(3, L_45);
		return L_46;
	}
}
// Method Definition Index: 93970
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR void XmlConvert__cctor_m7B068115A3F73B6954E126C91D02F3B338BCBC1C (const RuntimeMethod* method) 
{
	static bool s_Il2CppMethodInitialized;
	if (!s_Il2CppMethodInitialized)
	{
		il2cpp_codegen_initialize_runtime_metadata((uintptr_t*)&CharU5BU5D_t799905CF001DD5F13F7DBB310181FC4D8B7D0AAB_il2cpp_TypeInfo_var);
		il2cpp_codegen_initialize_runtime_metadata((uintptr_t*)&U3CPrivateImplementationDetailsU3E_tBAB6DC40B1709E21296656960228A17F1BE57FD7____DACC0E6568D9B10337A1A693F10D42C3175E80C378154406C6FBFA989F919F51_FieldInfo_var);
		il2cpp_codegen_initialize_runtime_metadata((uintptr_t*)&U3CPrivateImplementationDetailsU3E_tBAB6DC40B1709E21296656960228A17F1BE57FD7____DBBF94CCF4250B303976041627BB8FC302CE57D55373C469F41F97318412037D_FieldInfo_var);
		il2cpp_codegen_initialize_runtime_metadata((uintptr_t*)&XmlConvert_t5D100AFD62FF717BD4F8A6CABA9EDF7A5DF719EA_il2cpp_TypeInfo_var);
		s_Il2CppMethodInitialized = true;
	}
	{
		XmlCharType_t7C471894C8862126737E800F5A14AACBD14FCBC3 L_0;
		L_0 = XmlCharType_get_Instance_m1279A3FC91B74599A9BC76979E50DBFC07F67C55(NULL);
		((XmlConvert_t5D100AFD62FF717BD4F8A6CABA9EDF7A5DF719EA_StaticFields*)il2cpp_codegen_static_fields_for(XmlConvert_t5D100AFD62FF717BD4F8A6CABA9EDF7A5DF719EA_il2cpp_TypeInfo_var))->___xmlCharType = L_0;
		Il2CppCodeGenWriteBarrier((void**)&(((&((XmlConvert_t5D100AFD62FF717BD4F8A6CABA9EDF7A5DF719EA_StaticFields*)il2cpp_codegen_static_fields_for(XmlConvert_t5D100AFD62FF717BD4F8A6CABA9EDF7A5DF719EA_il2cpp_TypeInfo_var))->___xmlCharType))->___charProperties), (void*)NULL);
		CharU5BU5D_t799905CF001DD5F13F7DBB310181FC4D8B7D0AAB* L_1 = (CharU5BU5D_t799905CF001DD5F13F7DBB310181FC4D8B7D0AAB*)(CharU5BU5D_t799905CF001DD5F13F7DBB310181FC4D8B7D0AAB*)SZArrayNew(CharU5BU5D_t799905CF001DD5F13F7DBB310181FC4D8B7D0AAB_il2cpp_TypeInfo_var, (uint32_t)3);
		CharU5BU5D_t799905CF001DD5F13F7DBB310181FC4D8B7D0AAB* L_2 = L_1;
		RuntimeFieldHandle_t6E4C45B6D2EA12FC99185805A7E77527899B25C5 L_3 = { reinterpret_cast<intptr_t> (U3CPrivateImplementationDetailsU3E_tBAB6DC40B1709E21296656960228A17F1BE57FD7____DBBF94CCF4250B303976041627BB8FC302CE57D55373C469F41F97318412037D_FieldInfo_var) };
		RuntimeHelpers_InitializeArray_m751372AA3F24FBF6DA9B9D687CBFA2DE436CAB9B((RuntimeArray*)L_2, L_3, NULL);
		((XmlConvert_t5D100AFD62FF717BD4F8A6CABA9EDF7A5DF719EA_StaticFields*)il2cpp_codegen_static_fields_for(XmlConvert_t5D100AFD62FF717BD4F8A6CABA9EDF7A5DF719EA_il2cpp_TypeInfo_var))->___crt = L_2;
		Il2CppCodeGenWriteBarrier((void**)(&((XmlConvert_t5D100AFD62FF717BD4F8A6CABA9EDF7A5DF719EA_StaticFields*)il2cpp_codegen_static_fields_for(XmlConvert_t5D100AFD62FF717BD4F8A6CABA9EDF7A5DF719EA_il2cpp_TypeInfo_var))->___crt), (void*)L_2);
		((XmlConvert_t5D100AFD62FF717BD4F8A6CABA9EDF7A5DF719EA_StaticFields*)il2cpp_codegen_static_fields_for(XmlConvert_t5D100AFD62FF717BD4F8A6CABA9EDF7A5DF719EA_il2cpp_TypeInfo_var))->___c_EncodedCharLength = 7;
		CharU5BU5D_t799905CF001DD5F13F7DBB310181FC4D8B7D0AAB* L_4 = (CharU5BU5D_t799905CF001DD5F13F7DBB310181FC4D8B7D0AAB*)(CharU5BU5D_t799905CF001DD5F13F7DBB310181FC4D8B7D0AAB*)SZArrayNew(CharU5BU5D_t799905CF001DD5F13F7DBB310181FC4D8B7D0AAB_il2cpp_TypeInfo_var, (uint32_t)4);
		CharU5BU5D_t799905CF001DD5F13F7DBB310181FC4D8B7D0AAB* L_5 = L_4;
		RuntimeFieldHandle_t6E4C45B6D2EA12FC99185805A7E77527899B25C5 L_6 = { reinterpret_cast<intptr_t> (U3CPrivateImplementationDetailsU3E_tBAB6DC40B1709E21296656960228A17F1BE57FD7____DACC0E6568D9B10337A1A693F10D42C3175E80C378154406C6FBFA989F919F51_FieldInfo_var) };
		RuntimeHelpers_InitializeArray_m751372AA3F24FBF6DA9B9D687CBFA2DE436CAB9B((RuntimeArray*)L_5, L_6, NULL);
		((XmlConvert_t5D100AFD62FF717BD4F8A6CABA9EDF7A5DF719EA_StaticFields*)il2cpp_codegen_static_fields_for(XmlConvert_t5D100AFD62FF717BD4F8A6CABA9EDF7A5DF719EA_il2cpp_TypeInfo_var))->___WhitespaceChars = L_5;
		Il2CppCodeGenWriteBarrier((void**)(&((XmlConvert_t5D100AFD62FF717BD4F8A6CABA9EDF7A5DF719EA_StaticFields*)il2cpp_codegen_static_fields_for(XmlConvert_t5D100AFD62FF717BD4F8A6CABA9EDF7A5DF719EA_il2cpp_TypeInfo_var))->___WhitespaceChars), (void*)L_5);
		return;
	}
}
#ifdef __clang__
#pragma clang diagnostic pop
#endif
#ifdef __clang__
#pragma clang diagnostic push
#pragma clang diagnostic ignored "-Winvalid-offsetof"
#pragma clang diagnostic ignored "-Wunused-variable"
#endif
#ifdef __clang__
#pragma clang diagnostic pop
#endif
#ifdef __clang__
#pragma clang diagnostic push
#pragma clang diagnostic ignored "-Winvalid-offsetof"
#pragma clang diagnostic ignored "-Wunused-variable"
#endif
#ifdef __clang__
#pragma clang diagnostic pop
#endif
#ifdef __clang__
#pragma clang diagnostic push
#pragma clang diagnostic ignored "-Winvalid-offsetof"
#pragma clang diagnostic ignored "-Wunused-variable"
#endif
// Method Definition Index: 93972
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR String_t* Res_GetString_mBEE82ACFE97F93A6DDE81E322477CF2713466364 (String_t* ___0_name, const RuntimeMethod* method) 
{
	{
		String_t* L_0 = ___0_name;
		return L_0;
	}
}
#ifdef __clang__
#pragma clang diagnostic pop
#endif
#ifdef __clang__
#pragma clang diagnostic push
#pragma clang diagnostic ignored "-Winvalid-offsetof"
#pragma clang diagnostic ignored "-Wunused-variable"
#endif
// Method Definition Index: 93973
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR void XmlSchemaAnnotated__ctor_m094C7E3CD496FCF801D2A9A68E6D410B634096BC (XmlSchemaAnnotated_t41DBE9DF8776C6C639B33A712F25497C672B4E04* __this, const RuntimeMethod* method) 
{
	{
		XmlSchemaObject__ctor_m72D9D6DC7E5B4FBB6ADEB97F688474AF8AC7CADE(__this, NULL);
		return;
	}
}
#ifdef __clang__
#pragma clang diagnostic pop
#endif
#ifdef __clang__
#pragma clang diagnostic push
#pragma clang diagnostic ignored "-Winvalid-offsetof"
#pragma clang diagnostic ignored "-Wunused-variable"
#endif
#ifdef __clang__
#pragma clang diagnostic pop
#endif
#ifdef __clang__
#pragma clang diagnostic push
#pragma clang diagnostic ignored "-Winvalid-offsetof"
#pragma clang diagnostic ignored "-Wunused-variable"
#endif
#ifdef __clang__
#pragma clang diagnostic pop
#endif
#ifdef __clang__
#pragma clang diagnostic push
#pragma clang diagnostic ignored "-Winvalid-offsetof"
#pragma clang diagnostic ignored "-Wunused-variable"
#endif
#ifdef __clang__
#pragma clang diagnostic pop
#endif
#ifdef __clang__
#pragma clang diagnostic push
#pragma clang diagnostic ignored "-Winvalid-offsetof"
#pragma clang diagnostic ignored "-Wunused-variable"
#endif
// Method Definition Index: 93974
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR void XmlSchemaInfo__ctor_mEEC95F1F490C5D62BF1CC6DB16FE4938E5C1C341 (XmlSchemaInfo_t6686870BCAA40A8EFD7987976F386DD40DD5323B* __this, const RuntimeMethod* method) 
{
	{
		Object__ctor_mE837C6B9FA8C6D5D109F4B2EC885D79919AC0EA2(__this, NULL);
		XmlSchemaInfo_Clear_m6EACF6A1BB70B47CBC2865B8CCC74BEA67F96FFE(__this, NULL);
		return;
	}
}
// Method Definition Index: 93975
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR void XmlSchemaInfo__ctor_mEB56BD698D44A8AAA47668230E0CDFE8463ABD08 (XmlSchemaInfo_t6686870BCAA40A8EFD7987976F386DD40DD5323B* __this, int32_t ___0_validity, const RuntimeMethod* method) 
{
	{
		XmlSchemaInfo__ctor_mEEC95F1F490C5D62BF1CC6DB16FE4938E5C1C341(__this, NULL);
		int32_t L_0 = ___0_validity;
		__this->___validity = L_0;
		return;
	}
}
// Method Definition Index: 93976
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR void XmlSchemaInfo_Clear_m6EACF6A1BB70B47CBC2865B8CCC74BEA67F96FFE (XmlSchemaInfo_t6686870BCAA40A8EFD7987976F386DD40DD5323B* __this, const RuntimeMethod* method) 
{
	{
		__this->___isNil = (bool)0;
		__this->___isDefault = (bool)0;
		__this->___schemaType = (XmlSchemaType_t36630F421D730DDFA33CBA5BA6548094ABF40DDF*)NULL;
		Il2CppCodeGenWriteBarrier((void**)(&__this->___schemaType), (void*)(XmlSchemaType_t36630F421D730DDFA33CBA5BA6548094ABF40DDF*)NULL);
		__this->___schemaElement = (XmlSchemaElement_t24D5940E4EB4E5FFDAD1772AFBD7A83CD992C970*)NULL;
		Il2CppCodeGenWriteBarrier((void**)(&__this->___schemaElement), (void*)(XmlSchemaElement_t24D5940E4EB4E5FFDAD1772AFBD7A83CD992C970*)NULL);
		__this->___schemaAttribute = (XmlSchemaAttribute_t04AF3100B886867E3F0495E22F44305EDEC5550F*)NULL;
		Il2CppCodeGenWriteBarrier((void**)(&__this->___schemaAttribute), (void*)(XmlSchemaAttribute_t04AF3100B886867E3F0495E22F44305EDEC5550F*)NULL);
		__this->___memberType = (XmlSchemaSimpleType_tA52BBE2D61DAD61BD3FCDF40E1CD9F41388F7FA8*)NULL;
		Il2CppCodeGenWriteBarrier((void**)(&__this->___memberType), (void*)(XmlSchemaSimpleType_tA52BBE2D61DAD61BD3FCDF40E1CD9F41388F7FA8*)NULL);
		__this->___validity = 0;
		__this->___contentType = 1;
		return;
	}
}
#ifdef __clang__
#pragma clang diagnostic pop
#endif
#ifdef __clang__
#pragma clang diagnostic push
#pragma clang diagnostic ignored "-Winvalid-offsetof"
#pragma clang diagnostic ignored "-Wunused-variable"
#endif
// Method Definition Index: 93977
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR void XmlSchemaObject__ctor_m72D9D6DC7E5B4FBB6ADEB97F688474AF8AC7CADE (XmlSchemaObject_t82CCD87AADB4BEF4E9DFE4C501F09EDA372AA19D* __this, const RuntimeMethod* method) 
{
	{
		Object__ctor_mE837C6B9FA8C6D5D109F4B2EC885D79919AC0EA2(__this, NULL);
		return;
	}
}
#ifdef __clang__
#pragma clang diagnostic pop
#endif
#ifdef __clang__
#pragma clang diagnostic push
#pragma clang diagnostic ignored "-Winvalid-offsetof"
#pragma clang diagnostic ignored "-Wunused-variable"
#endif
// Method Definition Index: 93978
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR void XmlSchemaParticle__ctor_mDF61E3EE9B7D4102592DA20D02619EBF4D04F9A1 (XmlSchemaParticle_t9A7544A3F66C6E4B8900456CF705A6F9D95655EE* __this, const RuntimeMethod* method) 
{
	static bool s_Il2CppMethodInitialized;
	if (!s_Il2CppMethodInitialized)
	{
		il2cpp_codegen_initialize_runtime_metadata((uintptr_t*)&Decimal_tDA6C877282B2D789CF97C0949661CC11D643969F_il2cpp_TypeInfo_var);
		s_Il2CppMethodInitialized = true;
	}
	{
		il2cpp_codegen_runtime_class_init_inline(Decimal_tDA6C877282B2D789CF97C0949661CC11D643969F_il2cpp_TypeInfo_var);
		Decimal_tDA6C877282B2D789CF97C0949661CC11D643969F L_0 = ((Decimal_tDA6C877282B2D789CF97C0949661CC11D643969F_StaticFields*)il2cpp_codegen_static_fields_for(Decimal_tDA6C877282B2D789CF97C0949661CC11D643969F_il2cpp_TypeInfo_var))->___One;
		__this->___minOccurs = L_0;
		Decimal_tDA6C877282B2D789CF97C0949661CC11D643969F L_1 = ((Decimal_tDA6C877282B2D789CF97C0949661CC11D643969F_StaticFields*)il2cpp_codegen_static_fields_for(Decimal_tDA6C877282B2D789CF97C0949661CC11D643969F_il2cpp_TypeInfo_var))->___One;
		__this->___maxOccurs = L_1;
		XmlSchemaAnnotated__ctor_m094C7E3CD496FCF801D2A9A68E6D410B634096BC(__this, NULL);
		return;
	}
}
// Method Definition Index: 93979
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR void XmlSchemaParticle__cctor_mC201652B51B111B023A14925C46E82E714A1CA0F (const RuntimeMethod* method) 
{
	static bool s_Il2CppMethodInitialized;
	if (!s_Il2CppMethodInitialized)
	{
		il2cpp_codegen_initialize_runtime_metadata((uintptr_t*)&EmptyParticle_tB70737785BCC28912378BA174C18956FF658454A_il2cpp_TypeInfo_var);
		il2cpp_codegen_initialize_runtime_metadata((uintptr_t*)&XmlSchemaParticle_t9A7544A3F66C6E4B8900456CF705A6F9D95655EE_il2cpp_TypeInfo_var);
		s_Il2CppMethodInitialized = true;
	}
	{
		EmptyParticle_tB70737785BCC28912378BA174C18956FF658454A* L_0 = (EmptyParticle_tB70737785BCC28912378BA174C18956FF658454A*)il2cpp_codegen_object_new(EmptyParticle_tB70737785BCC28912378BA174C18956FF658454A_il2cpp_TypeInfo_var);
		EmptyParticle__ctor_m7FE5C70CB31BBBE2FAF38D403AC196F379DAC079(L_0, NULL);
		((XmlSchemaParticle_t9A7544A3F66C6E4B8900456CF705A6F9D95655EE_StaticFields*)il2cpp_codegen_static_fields_for(XmlSchemaParticle_t9A7544A3F66C6E4B8900456CF705A6F9D95655EE_il2cpp_TypeInfo_var))->___Empty = L_0;
		Il2CppCodeGenWriteBarrier((void**)(&((XmlSchemaParticle_t9A7544A3F66C6E4B8900456CF705A6F9D95655EE_StaticFields*)il2cpp_codegen_static_fields_for(XmlSchemaParticle_t9A7544A3F66C6E4B8900456CF705A6F9D95655EE_il2cpp_TypeInfo_var))->___Empty), (void*)L_0);
		return;
	}
}
#ifdef __clang__
#pragma clang diagnostic pop
#endif
#ifdef __clang__
#pragma clang diagnostic push
#pragma clang diagnostic ignored "-Winvalid-offsetof"
#pragma clang diagnostic ignored "-Wunused-variable"
#endif
// Method Definition Index: 93980
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR void EmptyParticle__ctor_m7FE5C70CB31BBBE2FAF38D403AC196F379DAC079 (EmptyParticle_tB70737785BCC28912378BA174C18956FF658454A* __this, const RuntimeMethod* method) 
{
	static bool s_Il2CppMethodInitialized;
	if (!s_Il2CppMethodInitialized)
	{
		il2cpp_codegen_initialize_runtime_metadata((uintptr_t*)&XmlSchemaParticle_t9A7544A3F66C6E4B8900456CF705A6F9D95655EE_il2cpp_TypeInfo_var);
		s_Il2CppMethodInitialized = true;
	}
	{
		il2cpp_codegen_runtime_class_init_inline(XmlSchemaParticle_t9A7544A3F66C6E4B8900456CF705A6F9D95655EE_il2cpp_TypeInfo_var);
		XmlSchemaParticle__ctor_mDF61E3EE9B7D4102592DA20D02619EBF4D04F9A1(__this, NULL);
		return;
	}
}
#ifdef __clang__
#pragma clang diagnostic pop
#endif
#ifdef __clang__
#pragma clang diagnostic push
#pragma clang diagnostic ignored "-Winvalid-offsetof"
#pragma clang diagnostic ignored "-Wunused-variable"
#endif
#ifdef __clang__
#pragma clang diagnostic pop
#endif
#ifdef __clang__
#pragma clang diagnostic push
#pragma clang diagnostic ignored "-Winvalid-offsetof"
#pragma clang diagnostic ignored "-Wunused-variable"
#endif
#ifdef __clang__
#pragma clang diagnostic pop
#endif
#ifdef __clang__
#pragma clang diagnostic push
#pragma clang diagnostic ignored "-Winvalid-offsetof"
#pragma clang diagnostic ignored "-Wunused-variable"
#endif
#ifdef __clang__
#pragma clang diagnostic pop
#endif
#ifdef __clang__
#pragma clang diagnostic push
#pragma clang diagnostic ignored "-Winvalid-offsetof"
#pragma clang diagnostic ignored "-Wunused-variable"
#endif
#ifdef __clang__
#pragma clang diagnostic pop
#endif
#ifdef __clang__
#pragma clang diagnostic push
#pragma clang diagnostic ignored "-Winvalid-offsetof"
#pragma clang diagnostic ignored "-Wunused-variable"
#endif
#ifdef __clang__
#pragma clang diagnostic pop
#endif
// Method Definition Index: 697
IL2CPP_MANAGED_FORCE_INLINE IL2CPP_METHOD_ATTR int32_t String_get_Length_m42625D67623FA5CC7A44D47425CE86FB946542D2_inline (String_t* __this, const RuntimeMethod* method) 
{
	{
		int32_t L_0 = __this->____stringLength;
		return L_0;
	}
}
// Method Definition Index: 93867
IL2CPP_MANAGED_FORCE_INLINE IL2CPP_METHOD_ATTR void XmlReaderDebuggerDisplayProxy__ctor_m56B82747E748FBB7BC8D0A1728D526EA23B54C1A_inline (XmlReaderDebuggerDisplayProxy_tCBCA1821E9EDC752D1884A7A4343C3B0C7134AC9* __this, XmlReader_t4C709DEF5F01606ECB60B638F1BD6F6E0A9116FD* ___0_reader, const RuntimeMethod* method) 
{
	{
		XmlReader_t4C709DEF5F01606ECB60B638F1BD6F6E0A9116FD* L_0 = ___0_reader;
		__this->___reader = L_0;
		Il2CppCodeGenWriteBarrier((void**)(&__this->___reader), (void*)L_0);
		return;
	}
}
// Method Definition Index: 93925
IL2CPP_MANAGED_FORCE_INLINE IL2CPP_METHOD_ATTR int32_t XmlName_get_HashCode_m329A9FEAF3BDEAA5F1F40CB84F7E51E1BDE58B2D_inline (XmlName_t0704430D24D202146901D342E34D878246E14F33* __this, const RuntimeMethod* method) 
{
	{
		int32_t L_0 = __this->___hashCode;
		return L_0;
	}
}
// Method Definition Index: 93924
IL2CPP_MANAGED_FORCE_INLINE IL2CPP_METHOD_ATTR String_t* XmlName_get_LocalName_mD4DE8695A21CB393834F0DDFDD6D6B610E91A64D_inline (XmlName_t0704430D24D202146901D342E34D878246E14F33* __this, const RuntimeMethod* method) 
{
	{
		String_t* L_0 = __this->___localName;
		return L_0;
	}
}
// Method Definition Index: 93923
IL2CPP_MANAGED_FORCE_INLINE IL2CPP_METHOD_ATTR XmlNameTable_tBDBAACFF3DB40A8E6AF3BDC11F0FF166CF11ABB8* XmlImplementation_get_NameTable_m8951702F59619310BD4A9528D2FF471352518CDF_inline (XmlImplementation_t4B3F467B76BD95C919C40424196C55B38EEC0F4D* __this, const RuntimeMethod* method) 
{
	{
		XmlNameTable_tBDBAACFF3DB40A8E6AF3BDC11F0FF166CF11ABB8* L_0 = __this->___nameTable;
		return L_0;
	}
}
// Method Definition Index: 93952
IL2CPP_MANAGED_FORCE_INLINE IL2CPP_METHOD_ATTR XmlNode_t3180B9B3D5C36CD58F5327D9F13458E3B3F030AF* XmlNodeChangedEventArgs_get_Node_mB5FA92B160E0200CF56DFCBAE3FC40F0D5C9B138_inline (XmlNodeChangedEventArgs_t965F2E02F8433933D197A0D1AF1EFEEDB524DF3A* __this, const RuntimeMethod* method) 
{
	{
		XmlNode_t3180B9B3D5C36CD58F5327D9F13458E3B3F030AF* L_0 = __this->___node;
		return L_0;
	}
}
// Method Definition Index: 93951
IL2CPP_MANAGED_FORCE_INLINE IL2CPP_METHOD_ATTR int32_t XmlNodeChangedEventArgs_get_Action_mD307B00A56DE5422A4EE4915400EC966DF80BD04_inline (XmlNodeChangedEventArgs_t965F2E02F8433933D197A0D1AF1EFEEDB524DF3A* __this, const RuntimeMethod* method) 
{
	{
		int32_t L_0 = __this->___action;
		return L_0;
	}
}
// Method Definition Index: 93903
IL2CPP_MANAGED_FORCE_INLINE IL2CPP_METHOD_ATTR int32_t XmlElementList_get_ChangeCount_m6C653A302721EA1D8C41326E169F2ED54CD9EE34_inline (XmlElementList_tFE0D5337668FDE708796D7FDB669AE4A290CAB57* __this, const RuntimeMethod* method) 
{
	{
		int32_t L_0 = __this->___changeCount;
		return L_0;
	}
}
// Method Definition Index: 93949
IL2CPP_MANAGED_FORCE_INLINE IL2CPP_METHOD_ATTR void DebuggerDisplayXmlNodeProxy__ctor_mB9B6D906F2EBADBBDBA79FE184D6860363C32356_inline (DebuggerDisplayXmlNodeProxy_t75382D048931DB9A7D8EFB35A56EEC1D8241FB07* __this, XmlNode_t3180B9B3D5C36CD58F5327D9F13458E3B3F030AF* ___0_node, const RuntimeMethod* method) 
{
	{
		XmlNode_t3180B9B3D5C36CD58F5327D9F13458E3B3F030AF* L_0 = ___0_node;
		__this->___node = L_0;
		Il2CppCodeGenWriteBarrier((void**)(&__this->___node), (void*)L_0);
		return;
	}
}
// Method Definition Index: 93897
IL2CPP_MANAGED_FORCE_INLINE IL2CPP_METHOD_ATTR String_t* XmlDocumentType_get_SystemId_m17B225C509C0F5A4ECBB838AA81D15DED4016909_inline (XmlDocumentType_tFD7379E45412FC2EBD65280ED3DE7A1C941A8D9E* __this, const RuntimeMethod* method) 
{
	{
		String_t* L_0 = __this->___systemId;
		return L_0;
	}
}
// Method Definition Index: 93896
IL2CPP_MANAGED_FORCE_INLINE IL2CPP_METHOD_ATTR String_t* XmlDocumentType_get_PublicId_m9E81FAF2D0AA9DC847D118813CBEB085F0CE1955_inline (XmlDocumentType_tFD7379E45412FC2EBD65280ED3DE7A1C941A8D9E* __this, const RuntimeMethod* method) 
{
	{
		String_t* L_0 = __this->___publicId;
		return L_0;
	}
}
// Method Definition Index: 93898
IL2CPP_MANAGED_FORCE_INLINE IL2CPP_METHOD_ATTR String_t* XmlDocumentType_get_InternalSubset_mBD8D585ED0401EE1FC02F6FD5445E2109B083FEF_inline (XmlDocumentType_tFD7379E45412FC2EBD65280ED3DE7A1C941A8D9E* __this, const RuntimeMethod* method) 
{
	{
		String_t* L_0 = __this->___internalSubset;
		return L_0;
	}
}
// Method Definition Index: 93967
IL2CPP_MANAGED_FORCE_INLINE IL2CPP_METHOD_ATTR void XmlCharType__ctor_mB4A379AC51751894C31365E176E3B9BFB5ABC771_inline (XmlCharType_t7C471894C8862126737E800F5A14AACBD14FCBC3* __this, ByteU5BU5D_tA6237BF417AE52AD70CFB4EF24A7A82613DF9031* ___0_charProperties, const RuntimeMethod* method) 
{
	{
		ByteU5BU5D_tA6237BF417AE52AD70CFB4EF24A7A82613DF9031* L_0 = ___0_charProperties;
		__this->___charProperties = L_0;
		Il2CppCodeGenWriteBarrier((void**)(&__this->___charProperties), (void*)L_0);
		return;
	}
}
