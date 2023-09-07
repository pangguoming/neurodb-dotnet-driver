using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NeuroDB_DotNet_Driver
{
    public enum ResultStatus
    {
        ERROR_INFO=0,            /*错误消息*/
        PARSER_OK =1,            /*运行成功*/
        NO_MEM_ERR=2,            /*内存分配异常*/
        SYNTAX_ERR=3,           /*普通语法错误*/
        NO_Exp_ERR=4,           /*未找到此指令*/
        NO_LNK_ERR=5,             /*缺少关系*/
        NO_ARROW_ERR=6,          /*缺少箭头*/
        DOU_ARROW_ERR=7,         /*关系双箭头错误*/
        NO_HEAD_ERR=8,           /*缺少头节点*/
        NO_TAIL_ERR=9,         /*缺少尾结点*/
        CHAR_NUM_UL_ERR=10,      /*必须是字母数字下划线*/
        NOT_PATTERN_ERR=11,       /*不是模式表达式*/
        DUP_VAR_NM_ERR=12,       /*此变量已被使用*/
        NO_SM_TYPE_ERR=13,       /*数组中含有不相同的类型*/
        NO_SUP_TYPE=14,        /*不支持的数据类型*/
        WRON_EXP=15,           /*指令搭配错误*/
        NOT_SUPPORT=16,           /*暂不支持的指令*/
        WHERE_SYN_ERR=17,         /*where语句语法*/
        WHERE_RUN_ERR=18,         /*where运算语法*/
        NO_VAR_ERR=19,            /*未找到变量*/
        NO_PAIR_BRK=20,         /*缺失配对括号*/
        CLIST_HAS_LINK_ERR=21,  /*删除带有关边的节点*/
        CLIST_OPR_ERR=22,     /*数据操作错误*/
        ORDER_BY_SYN_ERR=23,   /*order by语句语法*/
        DEL_PATH_ERR=24,      /*不可删除路径*/
        UNDEFINED_VAR_ERR=25,    /*未定义的变量*/
        WHERE_PTN_NO_VAR_ERR=26,  /*where 模式条件，独立连通图缺少变量*/
        NO_PROC_ERR=27,      /*不支持的存储过程*/
        CSV_FILE_ERR=28,           /*csv文件读取错误*/
        CSV_ROW_VAR_ERR=29,   /*csv 变量属性名在列中未找到*/
        QUREY_TIMEOUT_ERR=30    /*查询过载超时*/
    //private Integer type;
    //private String name;

    //private ResultStatus(Integer type, String name)
    //{
    //    this.type = type;
    //    this.name = name;
    //}

    //public Integer getType()
    //{
    //    return type;
    //}

    //public String getName()
    //{
    //    return name;
    //}
    }
}
