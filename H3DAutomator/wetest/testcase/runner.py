# -*- coding: UTF-8 -*-
"""
Tencent is pleased to support the open source community by making GAutomator available.
Copyright (C) 2016 THL A29 Limited, a Tencent company. All rights reserved.
Licensed under the MIT License (the "License"); you may not use this file except in compliance with the License. You may obtain a copy of the License at
http://opensource.org/licenses/MIT
Unless required by applicable law or agreed to in writing, software distributed under the License is distributed on an "AS IS" basis, WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied. See the License for the specific language governing permissions and limitations under the License.

"""
__author__ = 'minhuaxu wukenaihesos@gmail.com'

import os
import random
import string
import datetime
from wpyscripts.tools.baisc_operater import *
import wpyscripts.tools.traverse.travel as travel

logger = manager.get_testcase_logger()


# 帮助文档及内容
# ==========================QQ登陆过程示例================================================
# def login_qq():
#     """
#         腾讯系游戏，通过QQ登陆
#
#         从拉起游戏出现QQ和微信登陆按钮--->QQ账号密码输入登陆-->登陆完成
#     :return:
#     """
#     tencent_login(scene_name="EmptyScene",login_button="/BootObj/CUIManager/Form_Login/LoginContainer/pnlMobileLogin/btnGroup/btnQQ",sleeptime=3)


def  find_and_click(path,max_count=10, sleeptime=3):
    element = None
    bound = None
    for i in range(max_count):
        try:
            element = engine.find_element(path)
            if element:
                bound = engine.get_element_bound(element)
        except WeTestRuntimeError as e:
            # 存在抛出异常的可能，比如说切换过程中，游戏可能并不在前台
            logger.warn(e)
            time.sleep(sleeptime)
        if element and bound:
            time.sleep(3)
            engine.click(bound)
            time.sleep(3)
            return
        else:
            time.sleep(sleeptime)


def handle_cmd_click(path):
    print 'finding element:' + path
    find_and_click(path,999999,5);

def handle_cmd_wait(sec):
    time.sleep(string.atof(sec))

def handle_cmd_enter_inner_game(levelid):
    print 'wait enter game...'
    stopBtn = find_elment_wait('/GameGlobal/UI Root/UIInnerGameMainLevel/TopRight/ComStopButton/StopButton',99999999,3)
    result = engine.call_registered_handler("EnableAutoFight", "")
    print 'enter game,enable auto fight' + result


def dispath_cmd(cmd,param):
    if(cmd == "click"):
        handle_cmd_click(param)
    if(cmd == "wait"):
        handle_cmd_wait(param)
    if(cmd == 'enter_inner_game'):
        handle_cmd_enter_inner_game(param)


def test_excute_autotest_script():
    f = open("auto.txt",'r')
    for line in f:
        line=line.strip('\n')
        print "excute" + line
        args = line.split(',')
        if(len(args) == 2):
            cmd_name = args[0]
            cmd_param = args[1]
            time.sleep(1)
            dispath_cmd(cmd_name,cmd_param)




def run():
    """
        业务逻辑的起点
    """
    try:
        #random_search_test()
        logger.info("=====================================================================")
        test_excute_autotest_script()
        pass
    except Exception as e:
        traceback.print_exc()
        stack = traceback.format_exc()
        logger.error(stack)
        report.report_error("script_error")
    finally:
        report.screenshot()
