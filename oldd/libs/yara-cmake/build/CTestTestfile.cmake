# CMake generated Testfile for 
# Source directory: W:/DP-Vystup/SW/DP/libs/yara-cmake/yara/cmake
# Build directory: W:/DP-Vystup/SW/DP/libs/yara-cmake/build
# 
# This file includes the relevant testing commands required for 
# testing this directory and lists subdirectories to be tested as well.
if("${CTEST_CONFIGURATION_TYPE}" MATCHES "^([Dd][Ee][Bb][Uu][Gg])$")
  add_test(test_alignment "W:/DP-Vystup/SW/DP/libs/yara-cmake/build/bin/Debug/alignment.exe")
  set_tests_properties(test_alignment PROPERTIES  _BACKTRACE_TRIPLES "W:/DP-Vystup/SW/DP/libs/yara-cmake/yara/cmake/yaratest.cmake;12;add_test;W:/DP-Vystup/SW/DP/libs/yara-cmake/yara/cmake/yaratest.cmake;0;;W:/DP-Vystup/SW/DP/libs/yara-cmake/yara/cmake/CMakeLists.txt;45;include;W:/DP-Vystup/SW/DP/libs/yara-cmake/yara/cmake/CMakeLists.txt;0;")
elseif("${CTEST_CONFIGURATION_TYPE}" MATCHES "^([Rr][Ee][Ll][Ee][Aa][Ss][Ee])$")
  add_test(test_alignment "W:/DP-Vystup/SW/DP/libs/yara-cmake/build/bin/Release/alignment.exe")
  set_tests_properties(test_alignment PROPERTIES  _BACKTRACE_TRIPLES "W:/DP-Vystup/SW/DP/libs/yara-cmake/yara/cmake/yaratest.cmake;12;add_test;W:/DP-Vystup/SW/DP/libs/yara-cmake/yara/cmake/yaratest.cmake;0;;W:/DP-Vystup/SW/DP/libs/yara-cmake/yara/cmake/CMakeLists.txt;45;include;W:/DP-Vystup/SW/DP/libs/yara-cmake/yara/cmake/CMakeLists.txt;0;")
elseif("${CTEST_CONFIGURATION_TYPE}" MATCHES "^([Mm][Ii][Nn][Ss][Ii][Zz][Ee][Rr][Ee][Ll])$")
  add_test(test_alignment "W:/DP-Vystup/SW/DP/libs/yara-cmake/build/bin/MinSizeRel/alignment.exe")
  set_tests_properties(test_alignment PROPERTIES  _BACKTRACE_TRIPLES "W:/DP-Vystup/SW/DP/libs/yara-cmake/yara/cmake/yaratest.cmake;12;add_test;W:/DP-Vystup/SW/DP/libs/yara-cmake/yara/cmake/yaratest.cmake;0;;W:/DP-Vystup/SW/DP/libs/yara-cmake/yara/cmake/CMakeLists.txt;45;include;W:/DP-Vystup/SW/DP/libs/yara-cmake/yara/cmake/CMakeLists.txt;0;")
elseif("${CTEST_CONFIGURATION_TYPE}" MATCHES "^([Rr][Ee][Ll][Ww][Ii][Tt][Hh][Dd][Ee][Bb][Ii][Nn][Ff][Oo])$")
  add_test(test_alignment "W:/DP-Vystup/SW/DP/libs/yara-cmake/build/bin/RelWithDebInfo/alignment.exe")
  set_tests_properties(test_alignment PROPERTIES  _BACKTRACE_TRIPLES "W:/DP-Vystup/SW/DP/libs/yara-cmake/yara/cmake/yaratest.cmake;12;add_test;W:/DP-Vystup/SW/DP/libs/yara-cmake/yara/cmake/yaratest.cmake;0;;W:/DP-Vystup/SW/DP/libs/yara-cmake/yara/cmake/CMakeLists.txt;45;include;W:/DP-Vystup/SW/DP/libs/yara-cmake/yara/cmake/CMakeLists.txt;0;")
else()
  add_test(test_alignment NOT_AVAILABLE)
endif()
if("${CTEST_CONFIGURATION_TYPE}" MATCHES "^([Dd][Ee][Bb][Uu][Gg])$")
  add_test(test_version "W:/DP-Vystup/SW/DP/libs/yara-cmake/build/bin/Debug/version.exe")
  set_tests_properties(test_version PROPERTIES  _BACKTRACE_TRIPLES "W:/DP-Vystup/SW/DP/libs/yara-cmake/yara/cmake/yaratest.cmake;21;add_test;W:/DP-Vystup/SW/DP/libs/yara-cmake/yara/cmake/yaratest.cmake;0;;W:/DP-Vystup/SW/DP/libs/yara-cmake/yara/cmake/CMakeLists.txt;45;include;W:/DP-Vystup/SW/DP/libs/yara-cmake/yara/cmake/CMakeLists.txt;0;")
elseif("${CTEST_CONFIGURATION_TYPE}" MATCHES "^([Rr][Ee][Ll][Ee][Aa][Ss][Ee])$")
  add_test(test_version "W:/DP-Vystup/SW/DP/libs/yara-cmake/build/bin/Release/version.exe")
  set_tests_properties(test_version PROPERTIES  _BACKTRACE_TRIPLES "W:/DP-Vystup/SW/DP/libs/yara-cmake/yara/cmake/yaratest.cmake;21;add_test;W:/DP-Vystup/SW/DP/libs/yara-cmake/yara/cmake/yaratest.cmake;0;;W:/DP-Vystup/SW/DP/libs/yara-cmake/yara/cmake/CMakeLists.txt;45;include;W:/DP-Vystup/SW/DP/libs/yara-cmake/yara/cmake/CMakeLists.txt;0;")
elseif("${CTEST_CONFIGURATION_TYPE}" MATCHES "^([Mm][Ii][Nn][Ss][Ii][Zz][Ee][Rr][Ee][Ll])$")
  add_test(test_version "W:/DP-Vystup/SW/DP/libs/yara-cmake/build/bin/MinSizeRel/version.exe")
  set_tests_properties(test_version PROPERTIES  _BACKTRACE_TRIPLES "W:/DP-Vystup/SW/DP/libs/yara-cmake/yara/cmake/yaratest.cmake;21;add_test;W:/DP-Vystup/SW/DP/libs/yara-cmake/yara/cmake/yaratest.cmake;0;;W:/DP-Vystup/SW/DP/libs/yara-cmake/yara/cmake/CMakeLists.txt;45;include;W:/DP-Vystup/SW/DP/libs/yara-cmake/yara/cmake/CMakeLists.txt;0;")
elseif("${CTEST_CONFIGURATION_TYPE}" MATCHES "^([Rr][Ee][Ll][Ww][Ii][Tt][Hh][Dd][Ee][Bb][Ii][Nn][Ff][Oo])$")
  add_test(test_version "W:/DP-Vystup/SW/DP/libs/yara-cmake/build/bin/RelWithDebInfo/version.exe")
  set_tests_properties(test_version PROPERTIES  _BACKTRACE_TRIPLES "W:/DP-Vystup/SW/DP/libs/yara-cmake/yara/cmake/yaratest.cmake;21;add_test;W:/DP-Vystup/SW/DP/libs/yara-cmake/yara/cmake/yaratest.cmake;0;;W:/DP-Vystup/SW/DP/libs/yara-cmake/yara/cmake/CMakeLists.txt;45;include;W:/DP-Vystup/SW/DP/libs/yara-cmake/yara/cmake/CMakeLists.txt;0;")
else()
  add_test(test_version NOT_AVAILABLE)
endif()
if("${CTEST_CONFIGURATION_TYPE}" MATCHES "^([Dd][Ee][Bb][Uu][Gg])$")
  add_test(test_atoms "W:/DP-Vystup/SW/DP/libs/yara-cmake/build/bin/Debug/atoms.exe")
  set_tests_properties(test_atoms PROPERTIES  WORKING_DIRECTORY "W:/DP-Vystup/SW/DP/libs/yara-cmake/yara/cmake/.." _BACKTRACE_TRIPLES "W:/DP-Vystup/SW/DP/libs/yara-cmake/yara/cmake/yaratest.cmake;35;add_test;W:/DP-Vystup/SW/DP/libs/yara-cmake/yara/cmake/yaratest.cmake;0;;W:/DP-Vystup/SW/DP/libs/yara-cmake/yara/cmake/CMakeLists.txt;45;include;W:/DP-Vystup/SW/DP/libs/yara-cmake/yara/cmake/CMakeLists.txt;0;")
elseif("${CTEST_CONFIGURATION_TYPE}" MATCHES "^([Rr][Ee][Ll][Ee][Aa][Ss][Ee])$")
  add_test(test_atoms "W:/DP-Vystup/SW/DP/libs/yara-cmake/build/bin/Release/atoms.exe")
  set_tests_properties(test_atoms PROPERTIES  WORKING_DIRECTORY "W:/DP-Vystup/SW/DP/libs/yara-cmake/yara/cmake/.." _BACKTRACE_TRIPLES "W:/DP-Vystup/SW/DP/libs/yara-cmake/yara/cmake/yaratest.cmake;35;add_test;W:/DP-Vystup/SW/DP/libs/yara-cmake/yara/cmake/yaratest.cmake;0;;W:/DP-Vystup/SW/DP/libs/yara-cmake/yara/cmake/CMakeLists.txt;45;include;W:/DP-Vystup/SW/DP/libs/yara-cmake/yara/cmake/CMakeLists.txt;0;")
elseif("${CTEST_CONFIGURATION_TYPE}" MATCHES "^([Mm][Ii][Nn][Ss][Ii][Zz][Ee][Rr][Ee][Ll])$")
  add_test(test_atoms "W:/DP-Vystup/SW/DP/libs/yara-cmake/build/bin/MinSizeRel/atoms.exe")
  set_tests_properties(test_atoms PROPERTIES  WORKING_DIRECTORY "W:/DP-Vystup/SW/DP/libs/yara-cmake/yara/cmake/.." _BACKTRACE_TRIPLES "W:/DP-Vystup/SW/DP/libs/yara-cmake/yara/cmake/yaratest.cmake;35;add_test;W:/DP-Vystup/SW/DP/libs/yara-cmake/yara/cmake/yaratest.cmake;0;;W:/DP-Vystup/SW/DP/libs/yara-cmake/yara/cmake/CMakeLists.txt;45;include;W:/DP-Vystup/SW/DP/libs/yara-cmake/yara/cmake/CMakeLists.txt;0;")
elseif("${CTEST_CONFIGURATION_TYPE}" MATCHES "^([Rr][Ee][Ll][Ww][Ii][Tt][Hh][Dd][Ee][Bb][Ii][Nn][Ff][Oo])$")
  add_test(test_atoms "W:/DP-Vystup/SW/DP/libs/yara-cmake/build/bin/RelWithDebInfo/atoms.exe")
  set_tests_properties(test_atoms PROPERTIES  WORKING_DIRECTORY "W:/DP-Vystup/SW/DP/libs/yara-cmake/yara/cmake/.." _BACKTRACE_TRIPLES "W:/DP-Vystup/SW/DP/libs/yara-cmake/yara/cmake/yaratest.cmake;35;add_test;W:/DP-Vystup/SW/DP/libs/yara-cmake/yara/cmake/yaratest.cmake;0;;W:/DP-Vystup/SW/DP/libs/yara-cmake/yara/cmake/CMakeLists.txt;45;include;W:/DP-Vystup/SW/DP/libs/yara-cmake/yara/cmake/CMakeLists.txt;0;")
else()
  add_test(test_atoms NOT_AVAILABLE)
endif()
if("${CTEST_CONFIGURATION_TYPE}" MATCHES "^([Dd][Ee][Bb][Uu][Gg])$")
  add_test(test_pe "W:/DP-Vystup/SW/DP/libs/yara-cmake/build/bin/Debug/pe.exe")
  set_tests_properties(test_pe PROPERTIES  WORKING_DIRECTORY "W:/DP-Vystup/SW/DP/libs/yara-cmake/yara/cmake/.." _BACKTRACE_TRIPLES "W:/DP-Vystup/SW/DP/libs/yara-cmake/yara/cmake/yaratest.cmake;55;add_test;W:/DP-Vystup/SW/DP/libs/yara-cmake/yara/cmake/yaratest.cmake;0;;W:/DP-Vystup/SW/DP/libs/yara-cmake/yara/cmake/CMakeLists.txt;45;include;W:/DP-Vystup/SW/DP/libs/yara-cmake/yara/cmake/CMakeLists.txt;0;")
elseif("${CTEST_CONFIGURATION_TYPE}" MATCHES "^([Rr][Ee][Ll][Ee][Aa][Ss][Ee])$")
  add_test(test_pe "W:/DP-Vystup/SW/DP/libs/yara-cmake/build/bin/Release/pe.exe")
  set_tests_properties(test_pe PROPERTIES  WORKING_DIRECTORY "W:/DP-Vystup/SW/DP/libs/yara-cmake/yara/cmake/.." _BACKTRACE_TRIPLES "W:/DP-Vystup/SW/DP/libs/yara-cmake/yara/cmake/yaratest.cmake;55;add_test;W:/DP-Vystup/SW/DP/libs/yara-cmake/yara/cmake/yaratest.cmake;0;;W:/DP-Vystup/SW/DP/libs/yara-cmake/yara/cmake/CMakeLists.txt;45;include;W:/DP-Vystup/SW/DP/libs/yara-cmake/yara/cmake/CMakeLists.txt;0;")
elseif("${CTEST_CONFIGURATION_TYPE}" MATCHES "^([Mm][Ii][Nn][Ss][Ii][Zz][Ee][Rr][Ee][Ll])$")
  add_test(test_pe "W:/DP-Vystup/SW/DP/libs/yara-cmake/build/bin/MinSizeRel/pe.exe")
  set_tests_properties(test_pe PROPERTIES  WORKING_DIRECTORY "W:/DP-Vystup/SW/DP/libs/yara-cmake/yara/cmake/.." _BACKTRACE_TRIPLES "W:/DP-Vystup/SW/DP/libs/yara-cmake/yara/cmake/yaratest.cmake;55;add_test;W:/DP-Vystup/SW/DP/libs/yara-cmake/yara/cmake/yaratest.cmake;0;;W:/DP-Vystup/SW/DP/libs/yara-cmake/yara/cmake/CMakeLists.txt;45;include;W:/DP-Vystup/SW/DP/libs/yara-cmake/yara/cmake/CMakeLists.txt;0;")
elseif("${CTEST_CONFIGURATION_TYPE}" MATCHES "^([Rr][Ee][Ll][Ww][Ii][Tt][Hh][Dd][Ee][Bb][Ii][Nn][Ff][Oo])$")
  add_test(test_pe "W:/DP-Vystup/SW/DP/libs/yara-cmake/build/bin/RelWithDebInfo/pe.exe")
  set_tests_properties(test_pe PROPERTIES  WORKING_DIRECTORY "W:/DP-Vystup/SW/DP/libs/yara-cmake/yara/cmake/.." _BACKTRACE_TRIPLES "W:/DP-Vystup/SW/DP/libs/yara-cmake/yara/cmake/yaratest.cmake;55;add_test;W:/DP-Vystup/SW/DP/libs/yara-cmake/yara/cmake/yaratest.cmake;0;;W:/DP-Vystup/SW/DP/libs/yara-cmake/yara/cmake/CMakeLists.txt;45;include;W:/DP-Vystup/SW/DP/libs/yara-cmake/yara/cmake/CMakeLists.txt;0;")
else()
  add_test(test_pe NOT_AVAILABLE)
endif()
if("${CTEST_CONFIGURATION_TYPE}" MATCHES "^([Dd][Ee][Bb][Uu][Gg])$")
  add_test(test_elf "W:/DP-Vystup/SW/DP/libs/yara-cmake/build/bin/Debug/elf.exe")
  set_tests_properties(test_elf PROPERTIES  WORKING_DIRECTORY "W:/DP-Vystup/SW/DP/libs/yara-cmake/yara/cmake/.." _BACKTRACE_TRIPLES "W:/DP-Vystup/SW/DP/libs/yara-cmake/yara/cmake/yaratest.cmake;64;add_test;W:/DP-Vystup/SW/DP/libs/yara-cmake/yara/cmake/yaratest.cmake;0;;W:/DP-Vystup/SW/DP/libs/yara-cmake/yara/cmake/CMakeLists.txt;45;include;W:/DP-Vystup/SW/DP/libs/yara-cmake/yara/cmake/CMakeLists.txt;0;")
elseif("${CTEST_CONFIGURATION_TYPE}" MATCHES "^([Rr][Ee][Ll][Ee][Aa][Ss][Ee])$")
  add_test(test_elf "W:/DP-Vystup/SW/DP/libs/yara-cmake/build/bin/Release/elf.exe")
  set_tests_properties(test_elf PROPERTIES  WORKING_DIRECTORY "W:/DP-Vystup/SW/DP/libs/yara-cmake/yara/cmake/.." _BACKTRACE_TRIPLES "W:/DP-Vystup/SW/DP/libs/yara-cmake/yara/cmake/yaratest.cmake;64;add_test;W:/DP-Vystup/SW/DP/libs/yara-cmake/yara/cmake/yaratest.cmake;0;;W:/DP-Vystup/SW/DP/libs/yara-cmake/yara/cmake/CMakeLists.txt;45;include;W:/DP-Vystup/SW/DP/libs/yara-cmake/yara/cmake/CMakeLists.txt;0;")
elseif("${CTEST_CONFIGURATION_TYPE}" MATCHES "^([Mm][Ii][Nn][Ss][Ii][Zz][Ee][Rr][Ee][Ll])$")
  add_test(test_elf "W:/DP-Vystup/SW/DP/libs/yara-cmake/build/bin/MinSizeRel/elf.exe")
  set_tests_properties(test_elf PROPERTIES  WORKING_DIRECTORY "W:/DP-Vystup/SW/DP/libs/yara-cmake/yara/cmake/.." _BACKTRACE_TRIPLES "W:/DP-Vystup/SW/DP/libs/yara-cmake/yara/cmake/yaratest.cmake;64;add_test;W:/DP-Vystup/SW/DP/libs/yara-cmake/yara/cmake/yaratest.cmake;0;;W:/DP-Vystup/SW/DP/libs/yara-cmake/yara/cmake/CMakeLists.txt;45;include;W:/DP-Vystup/SW/DP/libs/yara-cmake/yara/cmake/CMakeLists.txt;0;")
elseif("${CTEST_CONFIGURATION_TYPE}" MATCHES "^([Rr][Ee][Ll][Ww][Ii][Tt][Hh][Dd][Ee][Bb][Ii][Nn][Ff][Oo])$")
  add_test(test_elf "W:/DP-Vystup/SW/DP/libs/yara-cmake/build/bin/RelWithDebInfo/elf.exe")
  set_tests_properties(test_elf PROPERTIES  WORKING_DIRECTORY "W:/DP-Vystup/SW/DP/libs/yara-cmake/yara/cmake/.." _BACKTRACE_TRIPLES "W:/DP-Vystup/SW/DP/libs/yara-cmake/yara/cmake/yaratest.cmake;64;add_test;W:/DP-Vystup/SW/DP/libs/yara-cmake/yara/cmake/yaratest.cmake;0;;W:/DP-Vystup/SW/DP/libs/yara-cmake/yara/cmake/CMakeLists.txt;45;include;W:/DP-Vystup/SW/DP/libs/yara-cmake/yara/cmake/CMakeLists.txt;0;")
else()
  add_test(test_elf NOT_AVAILABLE)
endif()
if("${CTEST_CONFIGURATION_TYPE}" MATCHES "^([Dd][Ee][Bb][Uu][Gg])$")
  add_test(test_api "W:/DP-Vystup/SW/DP/libs/yara-cmake/build/bin/Debug/api.exe")
  set_tests_properties(test_api PROPERTIES  WORKING_DIRECTORY "W:/DP-Vystup/SW/DP/libs/yara-cmake/yara/cmake/.." _BACKTRACE_TRIPLES "W:/DP-Vystup/SW/DP/libs/yara-cmake/yara/cmake/yaratest.cmake;73;add_test;W:/DP-Vystup/SW/DP/libs/yara-cmake/yara/cmake/yaratest.cmake;0;;W:/DP-Vystup/SW/DP/libs/yara-cmake/yara/cmake/CMakeLists.txt;45;include;W:/DP-Vystup/SW/DP/libs/yara-cmake/yara/cmake/CMakeLists.txt;0;")
elseif("${CTEST_CONFIGURATION_TYPE}" MATCHES "^([Rr][Ee][Ll][Ee][Aa][Ss][Ee])$")
  add_test(test_api "W:/DP-Vystup/SW/DP/libs/yara-cmake/build/bin/Release/api.exe")
  set_tests_properties(test_api PROPERTIES  WORKING_DIRECTORY "W:/DP-Vystup/SW/DP/libs/yara-cmake/yara/cmake/.." _BACKTRACE_TRIPLES "W:/DP-Vystup/SW/DP/libs/yara-cmake/yara/cmake/yaratest.cmake;73;add_test;W:/DP-Vystup/SW/DP/libs/yara-cmake/yara/cmake/yaratest.cmake;0;;W:/DP-Vystup/SW/DP/libs/yara-cmake/yara/cmake/CMakeLists.txt;45;include;W:/DP-Vystup/SW/DP/libs/yara-cmake/yara/cmake/CMakeLists.txt;0;")
elseif("${CTEST_CONFIGURATION_TYPE}" MATCHES "^([Mm][Ii][Nn][Ss][Ii][Zz][Ee][Rr][Ee][Ll])$")
  add_test(test_api "W:/DP-Vystup/SW/DP/libs/yara-cmake/build/bin/MinSizeRel/api.exe")
  set_tests_properties(test_api PROPERTIES  WORKING_DIRECTORY "W:/DP-Vystup/SW/DP/libs/yara-cmake/yara/cmake/.." _BACKTRACE_TRIPLES "W:/DP-Vystup/SW/DP/libs/yara-cmake/yara/cmake/yaratest.cmake;73;add_test;W:/DP-Vystup/SW/DP/libs/yara-cmake/yara/cmake/yaratest.cmake;0;;W:/DP-Vystup/SW/DP/libs/yara-cmake/yara/cmake/CMakeLists.txt;45;include;W:/DP-Vystup/SW/DP/libs/yara-cmake/yara/cmake/CMakeLists.txt;0;")
elseif("${CTEST_CONFIGURATION_TYPE}" MATCHES "^([Rr][Ee][Ll][Ww][Ii][Tt][Hh][Dd][Ee][Bb][Ii][Nn][Ff][Oo])$")
  add_test(test_api "W:/DP-Vystup/SW/DP/libs/yara-cmake/build/bin/RelWithDebInfo/api.exe")
  set_tests_properties(test_api PROPERTIES  WORKING_DIRECTORY "W:/DP-Vystup/SW/DP/libs/yara-cmake/yara/cmake/.." _BACKTRACE_TRIPLES "W:/DP-Vystup/SW/DP/libs/yara-cmake/yara/cmake/yaratest.cmake;73;add_test;W:/DP-Vystup/SW/DP/libs/yara-cmake/yara/cmake/yaratest.cmake;0;;W:/DP-Vystup/SW/DP/libs/yara-cmake/yara/cmake/CMakeLists.txt;45;include;W:/DP-Vystup/SW/DP/libs/yara-cmake/yara/cmake/CMakeLists.txt;0;")
else()
  add_test(test_api NOT_AVAILABLE)
endif()
if("${CTEST_CONFIGURATION_TYPE}" MATCHES "^([Dd][Ee][Bb][Uu][Gg])$")
  add_test(test_bitmask "W:/DP-Vystup/SW/DP/libs/yara-cmake/build/bin/Debug/bitmask.exe")
  set_tests_properties(test_bitmask PROPERTIES  WORKING_DIRECTORY "W:/DP-Vystup/SW/DP/libs/yara-cmake/yara/cmake/.." _BACKTRACE_TRIPLES "W:/DP-Vystup/SW/DP/libs/yara-cmake/yara/cmake/yaratest.cmake;82;add_test;W:/DP-Vystup/SW/DP/libs/yara-cmake/yara/cmake/yaratest.cmake;0;;W:/DP-Vystup/SW/DP/libs/yara-cmake/yara/cmake/CMakeLists.txt;45;include;W:/DP-Vystup/SW/DP/libs/yara-cmake/yara/cmake/CMakeLists.txt;0;")
elseif("${CTEST_CONFIGURATION_TYPE}" MATCHES "^([Rr][Ee][Ll][Ee][Aa][Ss][Ee])$")
  add_test(test_bitmask "W:/DP-Vystup/SW/DP/libs/yara-cmake/build/bin/Release/bitmask.exe")
  set_tests_properties(test_bitmask PROPERTIES  WORKING_DIRECTORY "W:/DP-Vystup/SW/DP/libs/yara-cmake/yara/cmake/.." _BACKTRACE_TRIPLES "W:/DP-Vystup/SW/DP/libs/yara-cmake/yara/cmake/yaratest.cmake;82;add_test;W:/DP-Vystup/SW/DP/libs/yara-cmake/yara/cmake/yaratest.cmake;0;;W:/DP-Vystup/SW/DP/libs/yara-cmake/yara/cmake/CMakeLists.txt;45;include;W:/DP-Vystup/SW/DP/libs/yara-cmake/yara/cmake/CMakeLists.txt;0;")
elseif("${CTEST_CONFIGURATION_TYPE}" MATCHES "^([Mm][Ii][Nn][Ss][Ii][Zz][Ee][Rr][Ee][Ll])$")
  add_test(test_bitmask "W:/DP-Vystup/SW/DP/libs/yara-cmake/build/bin/MinSizeRel/bitmask.exe")
  set_tests_properties(test_bitmask PROPERTIES  WORKING_DIRECTORY "W:/DP-Vystup/SW/DP/libs/yara-cmake/yara/cmake/.." _BACKTRACE_TRIPLES "W:/DP-Vystup/SW/DP/libs/yara-cmake/yara/cmake/yaratest.cmake;82;add_test;W:/DP-Vystup/SW/DP/libs/yara-cmake/yara/cmake/yaratest.cmake;0;;W:/DP-Vystup/SW/DP/libs/yara-cmake/yara/cmake/CMakeLists.txt;45;include;W:/DP-Vystup/SW/DP/libs/yara-cmake/yara/cmake/CMakeLists.txt;0;")
elseif("${CTEST_CONFIGURATION_TYPE}" MATCHES "^([Rr][Ee][Ll][Ww][Ii][Tt][Hh][Dd][Ee][Bb][Ii][Nn][Ff][Oo])$")
  add_test(test_bitmask "W:/DP-Vystup/SW/DP/libs/yara-cmake/build/bin/RelWithDebInfo/bitmask.exe")
  set_tests_properties(test_bitmask PROPERTIES  WORKING_DIRECTORY "W:/DP-Vystup/SW/DP/libs/yara-cmake/yara/cmake/.." _BACKTRACE_TRIPLES "W:/DP-Vystup/SW/DP/libs/yara-cmake/yara/cmake/yaratest.cmake;82;add_test;W:/DP-Vystup/SW/DP/libs/yara-cmake/yara/cmake/yaratest.cmake;0;;W:/DP-Vystup/SW/DP/libs/yara-cmake/yara/cmake/CMakeLists.txt;45;include;W:/DP-Vystup/SW/DP/libs/yara-cmake/yara/cmake/CMakeLists.txt;0;")
else()
  add_test(test_bitmask NOT_AVAILABLE)
endif()
if("${CTEST_CONFIGURATION_TYPE}" MATCHES "^([Dd][Ee][Bb][Uu][Gg])$")
  add_test(test_math "W:/DP-Vystup/SW/DP/libs/yara-cmake/build/bin/Debug/math.exe")
  set_tests_properties(test_math PROPERTIES  WORKING_DIRECTORY "W:/DP-Vystup/SW/DP/libs/yara-cmake/yara/cmake/.." _BACKTRACE_TRIPLES "W:/DP-Vystup/SW/DP/libs/yara-cmake/yara/cmake/yaratest.cmake;91;add_test;W:/DP-Vystup/SW/DP/libs/yara-cmake/yara/cmake/yaratest.cmake;0;;W:/DP-Vystup/SW/DP/libs/yara-cmake/yara/cmake/CMakeLists.txt;45;include;W:/DP-Vystup/SW/DP/libs/yara-cmake/yara/cmake/CMakeLists.txt;0;")
elseif("${CTEST_CONFIGURATION_TYPE}" MATCHES "^([Rr][Ee][Ll][Ee][Aa][Ss][Ee])$")
  add_test(test_math "W:/DP-Vystup/SW/DP/libs/yara-cmake/build/bin/Release/math.exe")
  set_tests_properties(test_math PROPERTIES  WORKING_DIRECTORY "W:/DP-Vystup/SW/DP/libs/yara-cmake/yara/cmake/.." _BACKTRACE_TRIPLES "W:/DP-Vystup/SW/DP/libs/yara-cmake/yara/cmake/yaratest.cmake;91;add_test;W:/DP-Vystup/SW/DP/libs/yara-cmake/yara/cmake/yaratest.cmake;0;;W:/DP-Vystup/SW/DP/libs/yara-cmake/yara/cmake/CMakeLists.txt;45;include;W:/DP-Vystup/SW/DP/libs/yara-cmake/yara/cmake/CMakeLists.txt;0;")
elseif("${CTEST_CONFIGURATION_TYPE}" MATCHES "^([Mm][Ii][Nn][Ss][Ii][Zz][Ee][Rr][Ee][Ll])$")
  add_test(test_math "W:/DP-Vystup/SW/DP/libs/yara-cmake/build/bin/MinSizeRel/math.exe")
  set_tests_properties(test_math PROPERTIES  WORKING_DIRECTORY "W:/DP-Vystup/SW/DP/libs/yara-cmake/yara/cmake/.." _BACKTRACE_TRIPLES "W:/DP-Vystup/SW/DP/libs/yara-cmake/yara/cmake/yaratest.cmake;91;add_test;W:/DP-Vystup/SW/DP/libs/yara-cmake/yara/cmake/yaratest.cmake;0;;W:/DP-Vystup/SW/DP/libs/yara-cmake/yara/cmake/CMakeLists.txt;45;include;W:/DP-Vystup/SW/DP/libs/yara-cmake/yara/cmake/CMakeLists.txt;0;")
elseif("${CTEST_CONFIGURATION_TYPE}" MATCHES "^([Rr][Ee][Ll][Ww][Ii][Tt][Hh][Dd][Ee][Bb][Ii][Nn][Ff][Oo])$")
  add_test(test_math "W:/DP-Vystup/SW/DP/libs/yara-cmake/build/bin/RelWithDebInfo/math.exe")
  set_tests_properties(test_math PROPERTIES  WORKING_DIRECTORY "W:/DP-Vystup/SW/DP/libs/yara-cmake/yara/cmake/.." _BACKTRACE_TRIPLES "W:/DP-Vystup/SW/DP/libs/yara-cmake/yara/cmake/yaratest.cmake;91;add_test;W:/DP-Vystup/SW/DP/libs/yara-cmake/yara/cmake/yaratest.cmake;0;;W:/DP-Vystup/SW/DP/libs/yara-cmake/yara/cmake/CMakeLists.txt;45;include;W:/DP-Vystup/SW/DP/libs/yara-cmake/yara/cmake/CMakeLists.txt;0;")
else()
  add_test(test_math NOT_AVAILABLE)
endif()
if("${CTEST_CONFIGURATION_TYPE}" MATCHES "^([Dd][Ee][Bb][Uu][Gg])$")
  add_test(test_stack "W:/DP-Vystup/SW/DP/libs/yara-cmake/build/bin/Debug/stack.exe")
  set_tests_properties(test_stack PROPERTIES  WORKING_DIRECTORY "W:/DP-Vystup/SW/DP/libs/yara-cmake/yara/cmake/.." _BACKTRACE_TRIPLES "W:/DP-Vystup/SW/DP/libs/yara-cmake/yara/cmake/yaratest.cmake;100;add_test;W:/DP-Vystup/SW/DP/libs/yara-cmake/yara/cmake/yaratest.cmake;0;;W:/DP-Vystup/SW/DP/libs/yara-cmake/yara/cmake/CMakeLists.txt;45;include;W:/DP-Vystup/SW/DP/libs/yara-cmake/yara/cmake/CMakeLists.txt;0;")
elseif("${CTEST_CONFIGURATION_TYPE}" MATCHES "^([Rr][Ee][Ll][Ee][Aa][Ss][Ee])$")
  add_test(test_stack "W:/DP-Vystup/SW/DP/libs/yara-cmake/build/bin/Release/stack.exe")
  set_tests_properties(test_stack PROPERTIES  WORKING_DIRECTORY "W:/DP-Vystup/SW/DP/libs/yara-cmake/yara/cmake/.." _BACKTRACE_TRIPLES "W:/DP-Vystup/SW/DP/libs/yara-cmake/yara/cmake/yaratest.cmake;100;add_test;W:/DP-Vystup/SW/DP/libs/yara-cmake/yara/cmake/yaratest.cmake;0;;W:/DP-Vystup/SW/DP/libs/yara-cmake/yara/cmake/CMakeLists.txt;45;include;W:/DP-Vystup/SW/DP/libs/yara-cmake/yara/cmake/CMakeLists.txt;0;")
elseif("${CTEST_CONFIGURATION_TYPE}" MATCHES "^([Mm][Ii][Nn][Ss][Ii][Zz][Ee][Rr][Ee][Ll])$")
  add_test(test_stack "W:/DP-Vystup/SW/DP/libs/yara-cmake/build/bin/MinSizeRel/stack.exe")
  set_tests_properties(test_stack PROPERTIES  WORKING_DIRECTORY "W:/DP-Vystup/SW/DP/libs/yara-cmake/yara/cmake/.." _BACKTRACE_TRIPLES "W:/DP-Vystup/SW/DP/libs/yara-cmake/yara/cmake/yaratest.cmake;100;add_test;W:/DP-Vystup/SW/DP/libs/yara-cmake/yara/cmake/yaratest.cmake;0;;W:/DP-Vystup/SW/DP/libs/yara-cmake/yara/cmake/CMakeLists.txt;45;include;W:/DP-Vystup/SW/DP/libs/yara-cmake/yara/cmake/CMakeLists.txt;0;")
elseif("${CTEST_CONFIGURATION_TYPE}" MATCHES "^([Rr][Ee][Ll][Ww][Ii][Tt][Hh][Dd][Ee][Bb][Ii][Nn][Ff][Oo])$")
  add_test(test_stack "W:/DP-Vystup/SW/DP/libs/yara-cmake/build/bin/RelWithDebInfo/stack.exe")
  set_tests_properties(test_stack PROPERTIES  WORKING_DIRECTORY "W:/DP-Vystup/SW/DP/libs/yara-cmake/yara/cmake/.." _BACKTRACE_TRIPLES "W:/DP-Vystup/SW/DP/libs/yara-cmake/yara/cmake/yaratest.cmake;100;add_test;W:/DP-Vystup/SW/DP/libs/yara-cmake/yara/cmake/yaratest.cmake;0;;W:/DP-Vystup/SW/DP/libs/yara-cmake/yara/cmake/CMakeLists.txt;45;include;W:/DP-Vystup/SW/DP/libs/yara-cmake/yara/cmake/CMakeLists.txt;0;")
else()
  add_test(test_stack NOT_AVAILABLE)
endif()
