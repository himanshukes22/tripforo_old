<?xml version="1.0" encoding="utf-8"?>
<xsl:stylesheet version="1.0" xmlns:xsl="http://www.w3.org/1999/XSL/Transform"
    xmlns:msxsl="urn:schemas-microsoft-com:xslt" exclude-result-prefixes="msxsl">
    <xsl:output method="xml" indent="yes"/>
  <xsl:template match="/Menus">
    <MenuItems>
      <xsl:call-template name="MenuListing" />
    </MenuItems>
  </xsl:template>
  <xsl:template name="MenuListing">
    <xsl:apply-templates select ="abc"/>
  </xsl:template>
  <xsl:template match="abc">
    <MenuItem>
      <xsl:attribute name="MenuText">
        <xsl:value-of select="PageName"/>
      </xsl:attribute>
      <xsl:attribute name="NavigateUrl">
        <xsl:value-of select="Page_URL"/>
      </xsl:attribute>
      <xsl:if test="count(abc)>0">
        <xsl:call-template name="MenuListing"/>
      </xsl:if>
    </MenuItem>
  </xsl:template>
</xsl:stylesheet>
